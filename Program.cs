using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using Mono.Cecil;
using System.Threading;
using System.Linq;
using Mono.Cecil.Cil;

using LazyString = System.Func<string>;
using static Actions;
using System.Collections.Generic;

public static class Variables
{
    public static char Temporary => 'T';
    public static char JumpOffset => 'J';

    public static char VariableStack => 'V';
    public static char VariableStackIndex => 'W';

    public static char ParameterStack => 'P';
    public static char ParameterStackIndex => 'Q';
}

// public struct WorkshopAction
// {
//     public string value;
//     public Func<string> getter;

//     public string Get() => value ?? getter();

//     public static implicit operator WorkshopAction(string v)
//     {
//         return new WorkshopAction { value = v };
//     }

//     public static implicit operator WorkshopAction(Func<string> g)
//     {
//         return new WorkshopAction { getter = g };
//     }
// }


public static class Actions
{
    public static LazyString GetGlobal(char variable)
    {
        return () => $"Global Variable({variable})";
    }

    public static LazyString SetGlobal(char variable, LazyString value)
    {
        return () => $"Set Global Variable({variable}, {value()});";
    }

    public static LazyString ArraySubscript(LazyString array, int index)
    {
        return ArraySubscript(array, () => index.ToString());
    }

    public static LazyString ArraySubscript(LazyString array, LazyString index)
    {
        return () => $"Value In Array({array()}, {index()})";
    }

    public static LazyString ArraySlice(LazyString array, LazyString startIndex, LazyString count)
    {
        return () => $"Array Slice({array()}, {startIndex()}, {count()})";
    }

    public static LazyString ArrayAppend(char arrayVar, LazyString value)
    {
        return SetGlobal(arrayVar, () => $"Append To Array({GetGlobal(arrayVar)()}, {value()})");
    }

    public static LazyString[] PushToStack(char stackVar, char stackIndexVar, LazyString value)
    {
        return new[] {
            ArrayAppend(stackVar, value),
            SetGlobal(stackIndexVar, () => $"Add(1, {GetGlobal(stackIndexVar)()})"),
        };
    }

    public static LazyString GetLastElementOfStack(char stackVar, char stackIndexVar, int offset)
    {
        return ArraySubscript(GetGlobal(stackVar), Add(GetGlobal(stackIndexVar), () => (-offset).ToString()));
    }

    public static LazyString[] ResizeStack(char stackVar, char stackIndexVar, LazyString size)
    {
        return new[] {
            ResizeArray(stackVar, size),
            SetGlobal(stackIndexVar, Add(size, () => "-1")),
        };
    }

    public static LazyString ResizeArray(char stackVar, LazyString size)
    {
        return SetGlobal(stackVar, ArraySlice(GetGlobal(stackVar), () => "0", size));
    }

    public static LazyString[] PopStack(char stackVar, char stackIndexVar, int count)
    {
        var newSize = Add(GetGlobal(stackIndexVar), () => (1 - count).ToString());
        return new[] {
            SetGlobal(stackVar, ArraySlice(GetGlobal(stackVar), () => "0", newSize)),
            SetGlobal(stackIndexVar, Add(newSize, () => "-1")),
        };
    }

    public static LazyString[] PushToVariableStack(LazyString value)
    {
        return PushToStack(Variables.VariableStack, Variables.VariableStackIndex, value);
    }

    public static LazyString[] PopVariableStack(int count)
    {
        return PopStack(Variables.VariableStack, Variables.VariableStackIndex, count);
    }

    public static LazyString GetLastElementOfVariableStack(int offset)
    {
        return GetLastElementOfStack(Variables.VariableStack, Variables.VariableStackIndex, offset);
    }

    public static LazyString[] PushToParameterStack(LazyString value)
    {
        return PushToStack(Variables.ParameterStack, Variables.ParameterStackIndex, value);
    }

    public static LazyString GetLastElementOfParameterStack(int offset)
    {
        return GetLastElementOfStack(Variables.ParameterStack, Variables.ParameterStackIndex, offset);
    }

    public static LazyString[] PopParameterStack(int count)
    {
        return PopStack(Variables.ParameterStack, Variables.ParameterStackIndex, count);
    }

    public static LazyString Add(LazyString valueA, LazyString valueB)
    {
        return () => $"Add({valueA()}, {valueB()})";
    }

    public static LazyString Subtract(LazyString valueA, LazyString valueB)
    {
        return () => $"Subtract({valueA()}, {valueB()})";
    }

    public static LazyString NotEqual(LazyString a, LazyString b)
    {
        return () => $"Compare({a()}, !=, {b()})";
    }

    public static LazyString Equal(LazyString a, LazyString b)
    {
        return () => $"Compare({a()}, ==, {b()})";
    }

    public static LazyString Skip(LazyString actionCount)
    {
        return () => $"Skip({actionCount()});";
    }

    public static LazyString SkipIf(LazyString value, LazyString actionCount)
    {
        return () => $"Skip If({value()}, {actionCount()});";
    }
}


class Program
{
    static LazyString[] MethodHeaderActions(MethodDefinition method)
    {
        var firstActions = new LazyString[]
        {
            () => "Abort If Condition Is False;",
            () => "Wait(0, Ignore Condition);",
        };

        var targets = method.Body.Instructions.
            Select(i => i.Operand as Instruction).
            Where(t => t != null).
            Distinct().
            ToArray();
        var numTargets = targets.Length;
        var targetJumps = targets.Select((target, index) => SkipIf(
            Equal(GetGlobal(Variables.JumpOffset), () => target.Offset.ToString()),
            () => (numTargets - index + CalcNumActionsToSkip(method, method.Body.Instructions[0], target)).ToString()));

        return firstActions.Concat(targetJumps).ToArray();
    }

    static IEnumerable<LazyString> ToWorkshopActions(MethodDefinition method, Instruction instruction)
    {
        if (instruction.OpCode == OpCodes.Ldarg_0)
            return PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 1));
        if (instruction.OpCode == OpCodes.Ldarg_1)
            return PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 2));
        if (instruction.OpCode == OpCodes.Ldarg_2)
            return PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 3));
        if (instruction.OpCode == OpCodes.Ldarg_3)
            return PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 4));

        if (instruction.OpCode == OpCodes.Add)
            return DoBinaryOp(Add);
        if (instruction.OpCode == OpCodes.Sub)
            return DoBinaryOp(Subtract);

        // TODO: pop parameters off stack and loop (put abort-if-false at top of function)
        if (instruction.OpCode == OpCodes.Ret)
            return new LazyString[] { () => "Abort;" };

        if (instruction.Operand is Instruction targetInstruction)
        {
            if (instruction.OpCode == OpCodes.Brtrue_S)
                return new LazyString[] {
                    SkipIf(Equal(GetLastElementOfVariableStack(0), () => "0"), () => "2"),
                    SetGlobal(Variables.JumpOffset, () => targetInstruction.Offset.ToString()),
                    () => "Loop;",
                };
                
            if (instruction.OpCode == OpCodes.Br_S)
                return new LazyString[] {
                    SetGlobal(Variables.JumpOffset, () => targetInstruction.Offset.ToString()),
                    () => "Loop;",
                };
        }

        if (instruction.OpCode == OpCodes.Ldc_I4_1)
            return PushToVariableStack(() => "1");
        if (instruction.OpCode == OpCodes.Ldc_R4)
            return PushToVariableStack(() => ((float)instruction.Operand).ToString());
        if (instruction.OpCode == OpCodes.Dup)
            return PushToVariableStack(GetLastElementOfVariableStack(0));

        // TODO: create local variables stack (using num loc variables from method def)
        if (instruction.OpCode == OpCodes.Stloc_0)
            return new[] { SetGlobal('A', GetLastElementOfVariableStack(0)) }.Concat(PopVariableStack(1)).ToArray();
        if (instruction.OpCode == OpCodes.Stloc_1)
            return new[] { SetGlobal('B', GetLastElementOfVariableStack(0)) }.Concat(PopVariableStack(1)).ToArray();
        if (instruction.OpCode == OpCodes.Ldloc_0)
            return PushToVariableStack(GetGlobal('A'));
        if (instruction.OpCode == OpCodes.Ldloc_1)
            return PushToVariableStack(GetGlobal('B'));

        if (instruction.OpCode == OpCodes.Starg_S)
        {
            return Starg(method, instruction);
        }

        if (instruction.OpCode == OpCodes.Call)
        {
            var targetMethod = (MethodDefinition)instruction.Operand;
            return new[] {
                // pop the parameters off the variable stack and onto the parameter stack
                ArrayAppend(Variables.ParameterStack, ArraySlice(
                    GetGlobal(Variables.VariableStack),
                    Subtract(GetGlobal(Variables.VariableStackIndex), () => (targetMethod.Parameters.Count - 1).ToString()),
                    () => targetMethod.Parameters.Count.ToString())),
                SetGlobal(Variables.ParameterStackIndex,
                    Add(GetGlobal(Variables.ParameterStackIndex), () => targetMethod.Parameters.Count.ToString())),
            }.Concat(
                PopStack(Variables.VariableStack, Variables.VariableStackIndex, targetMethod.Parameters.Count)
            ).ToArray();

            // TODO: set up functions to be called and call it
        }

        throw new Exception($"Unsupported opcode {instruction.OpCode}");
    }

    private static IEnumerable<LazyString> Starg(MethodDefinition method, Instruction instruction)
    {
        var param = (ParameterDefinition)instruction.Operand;
        var index = method.Parameters.IndexOf(param);
        yield return SetGlobal(Variables.Temporary, ArraySlice(
            GetGlobal(Variables.ParameterStack),
            Subtract(GetGlobal(Variables.ParameterStackIndex), () => (method.Parameters.Count - 1).ToString()),
            () => method.Parameters.Count.ToString()));

        foreach (var action in PopParameterStack(method.Parameters.Count))
            yield return action;
        
        foreach (var i in Enumerable.Range(0, method.Parameters.Count))
        {
            var push = PushToParameterStack(
                i == index ?
                GetLastElementOfVariableStack(0) :
                ArraySubscript(GetGlobal(Variables.Temporary), i));
            foreach (var action in push)
                yield return action;
        }

        foreach (var action in PopVariableStack(1))
            yield return action;
    }

    private static LazyString[] DoBinaryOp(Func<LazyString, LazyString, LazyString> binaryOp)
    {
        return new[]
        {
                // store the last two variables in temp
                SetGlobal(Variables.Temporary, ArraySubscript(GetGlobal(Variables.VariableStack), Add(GetGlobal(Variables.VariableStackIndex), () => "-1"))),
                ArrayAppend(Variables.Temporary, ArraySubscript(GetGlobal(Variables.VariableStack), GetGlobal(Variables.VariableStackIndex)))
            }.Concat(
            // pop them off the stack
            PopStack(Variables.VariableStack, Variables.VariableStackIndex, 2)
        ).Concat(
            // push the addition of them onto the stack
            PushToVariableStack(binaryOp(ArraySubscript(GetGlobal(Variables.Temporary), 0), ArraySubscript(GetGlobal(Variables.Temporary), 1)))
        ).ToArray();
    }

    private static int CalcNumActionsToSkip(MethodDefinition method, Instruction start, Instruction end)
    {
        return method.Body.Instructions.SkipWhile(i => i != start).TakeWhile(i => i != end).Sum(i => ToWorkshopActions(method, i).Count()) - 1;
    }

    static void Main(string[] args)
    {
        var source =
@"public static class Workshop {
    public static float Test(float a, float b)
    {
        return a + b;
    }

    public static float fib(int n)
    {
        var a = 0.0f;
        var b = 1.0f;
        while (n-- != 0) {
            var c = a + b;
            a = b;
            b = c;
        }
        return b;
    }

    // public static float fib(int n, float a = 0, float b = 1)
    // {
	// 	if (n == 0)
    //         return b;
    //     return fib(n - 1, b, a + b);
    // }
    
    public static void Main()
    {
        //Console.WriteLine(fib(10));
    }
}";

        var references = new[]
        {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            };

        var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release);
        var compilation = CSharpCompilation.Create(
            "AsmBuild",
            new[] { SyntaxFactory.ParseSyntaxTree(source) },
            references,
            options);
        var result = compilation.Emit("AsmBuild.dll");

        var module = ModuleDefinition.ReadModule("AsmBuild.dll");
        // var method = module.GetType("Workshop").Methods.First(m => m.Name == "Test");
        var method = module.GetType("Workshop").Methods.First(m => m.Name == "fib");

        foreach (var instr in method.Body.Instructions)
            Console.WriteLine(instr);
        Console.WriteLine();

        Console.WriteLine("// Header");
        foreach (var line in MethodHeaderActions(method))
            Console.WriteLine(line());
        Console.WriteLine();

        Console.WriteLine("// Body");
        foreach (var instr in method.Body.Instructions)
        {
            Console.WriteLine($"// {instr}");
            foreach (var line in ToWorkshopActions(method, instr))
                Console.WriteLine(line());
        }
    }
}
