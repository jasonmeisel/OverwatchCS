using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using Mono.Cecil;
using System.Threading;
using System.Linq;
using Mono.Cecil.Cil;

public static class Variables
{
    public static char Temporary => 'T';

    public static char VariableStack => 'V';
    public static char VariableStackIndex => 'W';

    public static char ParameterStack => 'P';
    public static char ParameterStackIndex => 'Q';
}

public static class Actions
{
    public static string GetGlobal(char variable)
    {
        return $"Global Variable({variable})";
    }

    public static string SetGlobal(char variable, string value)
    {
        return $"Set Global Variable({variable}, {value});";
    }

    public static string ArraySubscript(string array, int index)
    {
        return ArraySubscript(array, index.ToString());
    }

    public static string ArraySubscript(string array, string index)
    {
        return $"Value In Array({array}, {index})";
    }

    public static string ArrayAppend(char arrayVar, string value)
    {
        return SetGlobal(arrayVar, $"Append To Array({GetGlobal(arrayVar)}, {value})");
    }

    public static string[] PushToStack(char stackVar, char stackIndexVar, string value)
    {
        return new[] {
            ArrayAppend(stackVar, value),
            SetGlobal(stackIndexVar, $"Add(1, Global Variable({stackIndexVar}))"),
        };
    }

    public static string GetLastElementOfStack(char stackVar, char stackIndexVar)
    {
        return ArraySubscript(GetGlobal(stackVar), GetGlobal(stackIndexVar));
    }

    public static string[] ResizeStack(char stackVar, char stackIndexVar, string size)
    {
        return new[] {
            SetGlobal(stackVar, $"Array Slice({GetGlobal(stackVar)}, 0, {size})"),
            SetGlobal(stackIndexVar, Add(size, "-1")),
        };
    }

    public static string[] PopStack(char stackVar, char stackIndexVar, int count)
    {
        var newSize = Add(GetGlobal(stackIndexVar), (1 - count).ToString());
        return new[] {
            SetGlobal(stackVar, $"Array Slice({GetGlobal(stackVar)}, 0, {newSize})"),
            SetGlobal(stackIndexVar, Add(newSize, "-1")),
        };
    }

    public static string[] PushToVariableStack(string value)
    {
        return PushToStack(Variables.VariableStack, Variables.VariableStackIndex, value);
    }

    public static string GetLastElementOfVariableStack()
    {
        return GetLastElementOfStack(Variables.VariableStack, Variables.VariableStackIndex);
    }

    public static string[] PushToParameterStack(string value)
    {
        return PushToStack(Variables.ParameterStack, Variables.ParameterStackIndex, value);
    }

    public static string GetLastElementOfParameterStack()
    {
        return GetLastElementOfStack(Variables.ParameterStack, Variables.ParameterStackIndex);
    }

    public static string Add(string valueA, string valueB)
    {
        return $"Add({valueA}, {valueB})";
    }

    public static string[] ToWorkshopActions(Instruction instruction)
    {
        if (instruction.OpCode == OpCodes.Ldarg_0)
            return PushToVariableStack(ArraySubscript(GetLastElementOfParameterStack(), 0));
        if (instruction.OpCode == OpCodes.Ldarg_1)
            return PushToVariableStack(ArraySubscript(GetLastElementOfParameterStack(), 1));
        if (instruction.OpCode == OpCodes.Ldarg_2)
            return PushToVariableStack(ArraySubscript(GetLastElementOfParameterStack(), 2));
        if (instruction.OpCode == OpCodes.Ldarg_3)
            return PushToVariableStack(ArraySubscript(GetLastElementOfParameterStack(), 3));

        if (instruction.OpCode == OpCodes.Add)
            return new[]
            {
                // store the last two variables in temp
                SetGlobal(Variables.Temporary, ArraySubscript(GetGlobal(Variables.VariableStack), Add(GetGlobal(Variables.VariableStackIndex), "-1"))),
                ArrayAppend(Variables.Temporary, ArraySubscript(GetGlobal(Variables.VariableStack), GetGlobal(Variables.VariableStackIndex)))
            }.Concat(
                // pop them off the stack
                PopStack(Variables.VariableStack, Variables.VariableStackIndex, -2)
            ).Concat(
                // push the addition of them onto the stack
                PushToVariableStack(Add(ArraySubscript(GetGlobal(Variables.Temporary), 0), ArraySubscript(GetGlobal(Variables.Temporary), 1)))
            ).ToArray();

        if (instruction.OpCode == OpCodes.Ret)
            return new string[0];

        throw new Exception($"Unsupported opcode {instruction.OpCode}");
    }
}

namespace IL2Workshop
{

    class Program
    {
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
            var method = module.GetType("Workshop").Methods.First(m => m.Name == "Test");
            foreach (var instr in method.Body.Instructions)
                Console.WriteLine(instr);
            foreach (var instr in method.Body.Instructions)
                foreach (var line in Actions.ToWorkshopActions(instr))
                    Console.WriteLine(line);
        }
    }
}
