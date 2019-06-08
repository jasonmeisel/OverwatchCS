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

public static class Variables
{
    public static char Temporary => 'T';

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
            SetGlobal(stackVar, () => $"Array Slice({GetGlobal(stackVar)()}, 0, {size()})"),
            SetGlobal(stackIndexVar, Add(size, () => "-1")),
        };
    }

    public static LazyString[] PopStack(char stackVar, char stackIndexVar, int count)
    {
        var newSize = Add(GetGlobal(stackIndexVar), () => (1 - count).ToString());
        return new[] {
            SetGlobal(stackVar, () => $"Array Slice({GetGlobal(stackVar)()}, 0, {newSize()})"),
            SetGlobal(stackIndexVar, Add(newSize, () => "-1")),
        };
    }

    public static LazyString[] PushToVariableStack(LazyString value)
    {
        return PushToStack(Variables.VariableStack, Variables.VariableStackIndex, value);
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

    public static LazyString SkipIf(LazyString value, LazyString actionCount)
    {
        return () => $"Skip If({value()}, {actionCount()});";
    }
}


class Program
{

    public static LazyString[] ToWorkshopActions(MethodDefinition method, Instruction instruction)
    {
        if (instruction.OpCode == OpCodes.Ldarg_0)
            return PushToVariableStack(GetLastElementOfParameterStack(0));
        if (instruction.OpCode == OpCodes.Ldarg_1)
            return PushToVariableStack(GetLastElementOfParameterStack(1));
        if (instruction.OpCode == OpCodes.Ldarg_2)
            return PushToVariableStack(GetLastElementOfParameterStack(2));
        if (instruction.OpCode == OpCodes.Ldarg_3)
            return PushToVariableStack(GetLastElementOfParameterStack(3));

        if (instruction.OpCode == OpCodes.Add)
            return DoBinaryOp(Add);

        // TODO: pop parameters off stack and loop (put abort-if-false at top of function)
        if (instruction.OpCode == OpCodes.Ret)
            return new LazyString[] { () => "Abort;" };

        if (instruction.OpCode == OpCodes.Brtrue_S)
            return new[] { SkipIf(NotEqual(GetLastElementOfVariableStack(0), () => "0"), () => CalcNumActionsToSkip(method, instruction).ToString()) };

        if (instruction.OpCode == OpCodes.Ldc_I4_1)
            return PushToVariableStack(() => "1");

        if (instruction.OpCode == OpCodes.Sub)
            return DoBinaryOp(Subtract);

        throw new Exception($"Unsupported opcode {instruction.OpCode}");
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

    private static int CalcNumActionsToSkip(MethodDefinition method, Instruction instruction)
    {
        return method.Body.Instructions.SkipWhile(i => i != instruction).TakeWhile(i => i != instruction.Operand).Sum(i => ToWorkshopActions(method, i).Length) - 1;
    }

    static void Main(string[] args)
    {
        var source =
@"public static class Workshop {
    public static float Test(float a, float b)
    {
        return a + b;
    }

    public static float fib(int n, float a = 0, float b = 1)
    {
		if (n == 0)
            return b;
        return fib(n - 1, b, a + b);
    }
    
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
        foreach (var instr in method.Body.Instructions)
            foreach (var line in ToWorkshopActions(method, instr))
                Console.WriteLine(line());
    }
}
