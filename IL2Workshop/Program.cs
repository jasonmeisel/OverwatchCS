using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using Mono.Cecil;
using System.Threading;
using System.Linq;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.IO;

using LazyString = System.Func<string>;
using static Actions;
using ToWorkshopActionFunc = System.Func<Mono.Cecil.MethodDefinition, Mono.Cecil.Cil.Instruction, System.Collections.Generic.IEnumerable<System.Func<string>>>;

public static class Variables
{
    public static char Temporary => 'T';
    public static char JumpOffset => 'J';

    public static char CallStack => 'F';
    public static char CallStackIndex => 'G';

    public static char VariableStack => 'V';
    public static char VariableStackIndex => 'W';

    public static char ParameterStack => 'P';
    public static char ParameterStackIndex => 'Q';
}

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
        return SetGlobal(arrayVar, ArrayConcat(GetGlobal(arrayVar), value));
    }

    public static LazyString ArrayConcat(LazyString a, LazyString b)
    {
        return () => $"Append To Array({a()}, {b()})";
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

    static readonly Dictionary<OpCode, ToWorkshopActionFunc> s_toWorkshopActionsDict = CreateToWorkshopActionsDict();

    static Dictionary<OpCode, ToWorkshopActionFunc> CreateToWorkshopActionsDict()
    {
        var dict = new Dictionary<OpCode, ToWorkshopActionFunc>();

        dict[OpCodes.Ldarg_0] = (method, instruction) =>
            PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 1));
        dict[OpCodes.Ldarg_1] = (method, instruction) =>
            PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 2));
        dict[OpCodes.Ldarg_2] = (method, instruction) =>
            PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 3));
        dict[OpCodes.Ldarg_3] = (method, instruction) =>
            PushToVariableStack(GetLastElementOfParameterStack(method.Parameters.Count - 4));

        dict[OpCodes.Add] = (method, instruction) => DoBinaryOp(Add);
        dict[OpCodes.Sub] = (method, instruction) => DoBinaryOp(Subtract);

        // TODO: pop parameters off stack
        dict[OpCodes.Ret] = (method, instruction) => new[]
            {
                SetGlobal(Variables.CallStack, () => "2"),
                () => "Abort;"
            };

        // jump if true
        dict[OpCodes.Brtrue_S] = (method, instruction) =>
            new[] { SetGlobal(Variables.Temporary, GetLastElementOfVariableStack(0)) }.
                Concat(PopVariableStack(1)).
                Concat(new[]
                {
                    SkipIf(Equal(GetGlobal(Variables.Temporary), () => "0"), () => "2"),
                    SetGlobal(Variables.JumpOffset, () => ((Instruction)instruction.Operand).Offset.ToString()),
                    () => "Loop;",
                });

        // jump if not equal
        dict[OpCodes.Bne_Un_S] = (method, instruction) =>
            new[] {
                SetGlobal(Variables.Temporary, ArraySlice(
                    GetGlobal(Variables.VariableStack),
                    Subtract(GetGlobal(Variables.VariableStackIndex), () => "1"),
                    () => "2"))
                }.
                Concat(PopVariableStack(2)).
                Concat(new[]
                {
                    SkipIf(Equal(ArraySubscript(GetGlobal(Variables.Temporary), () => "0"), ArraySubscript(GetGlobal(Variables.Temporary), () => "1")), () => "2"),
                    SetGlobal(Variables.JumpOffset, () => ((Instruction)instruction.Operand).Offset.ToString()),
                    () => "Loop;",
                });

        // jump
        dict[OpCodes.Br_S] = (method, instruction) => new LazyString[]
            {
                SetGlobal(Variables.JumpOffset, () => ((Instruction)instruction.Operand).Offset.ToString()),
                () => "Loop;",
            };

        // shouldn't need to convert
        dict[OpCodes.Conv_R4] = (method, instruction) => new LazyString[0];

        dict[OpCodes.Ldc_I4_0] = (method, instruction) => PushToVariableStack(() => "0");
        dict[OpCodes.Ldc_I4_1] = (method, instruction) => PushToVariableStack(() => "1");
        dict[OpCodes.Ldc_R4] = (method, instruction) => PushToVariableStack(() => ((float)instruction.Operand).ToString());
        dict[OpCodes.Dup] = (method, instruction) => PushToVariableStack(GetLastElementOfVariableStack(0));

        // TODO: create local variables stack (using num loc variables from method def)
        dict[OpCodes.Stloc_0] = (method, instruction) =>
            new[] { SetGlobal('A', GetLastElementOfVariableStack(0)) }.Concat(PopVariableStack(1)).ToArray();
        dict[OpCodes.Stloc_1] = (method, instruction) =>
            new[] { SetGlobal('B', GetLastElementOfVariableStack(0)) }.Concat(PopVariableStack(1)).ToArray();
        dict[OpCodes.Ldloc_0] = (method, instruction) => PushToVariableStack(GetGlobal('A'));
        dict[OpCodes.Ldloc_1] = (method, instruction) => PushToVariableStack(GetGlobal('B'));

        dict[OpCodes.Starg_S] = Impl_Starg_S;
        dict[OpCodes.Call] = Impl_Call;

        return dict;
    }

    private static IEnumerable<LazyString> Impl_Call(MethodDefinition method, Instruction instruction)
    {
        if (instruction.Operand is MethodReference targetMethodRef)
            return CallWorkshopAction(method, instruction, targetMethodRef);

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

    static IEnumerable<LazyString> CallWorkshopAction(MethodDefinition method, Instruction instruction, MethodReference targetMethodRef)
    {
        // TODO: assert that the method reference is a workshop action

        switch (targetMethodRef.Name)
        {
            case "Wait":
                yield return () => $"Wait({GetLastElementOfVariableStack(0)()}, Ignore Condition);";
                break;
            case "DebugLog":
                yield return () => $"Big Message(All Players(All Teams), String(\"({{0}})\", {GetLastElementOfVariableStack(0)()}, Null, Null));";
                yield return () => "Wait(0, Ignore Condition);";
                break;
        }

        foreach (var action in PopVariableStack(targetMethodRef.Parameters.Count))
            yield return action;
    }

    static IEnumerable<LazyString> ToWorkshopActions(MethodDefinition method, Instruction instruction)
    {
        if (s_toWorkshopActionsDict.TryGetValue(instruction.OpCode, out var toActions))
            return toActions(method, instruction);
        throw new Exception($"Unsupported opcode {instruction.OpCode}");
    }

    private static IEnumerable<LazyString> Impl_Starg_S(MethodDefinition method, Instruction instruction)
    {
        yield return SetGlobal(Variables.Temporary, ArraySlice(
            GetGlobal(Variables.ParameterStack),
            Subtract(GetGlobal(Variables.ParameterStackIndex), () => (method.Parameters.Count - 1).ToString()),
            () => method.Parameters.Count.ToString()));

        foreach (var action in PopParameterStack(method.Parameters.Count))
            yield return action;

        foreach (var i in Enumerable.Range(0, method.Parameters.Count))
        {
            var push = PushToParameterStack(
                instruction.Operand == method.Parameters[i] ?
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
        var source = File.ReadAllText("Test\\Test.cs");
        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(Path.Combine(
                Path.GetDirectoryName(typeof(object).Assembly.Location),
                "System.Runtime.dll")),
            MetadataReference.CreateFromFile("C:\\projects\\IL2Workshop\\WorkshopStub\\bin\\Debug\\netcoreapp3.0\\WorkshopStub.dll"),
        };

        var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release);
        var compilation = CSharpCompilation.Create(
            "AsmBuild",
            new[] { SyntaxFactory.ParseSyntaxTree(source) },
            references,
            options);
        var result = compilation.Emit("AsmBuild.dll");
        if (!result.Success)
        {
            foreach (var diag in result.Diagnostics)
                Console.WriteLine(diag);
            return;
        }

        var module = ModuleDefinition.ReadModule("AsmBuild.dll");

        var ruleWriter = new StringWriter();
        foreach (var method in module.GetType("MainClass").Methods)
        {
            foreach (var instr in method.Body.Instructions)
                Console.WriteLine(instr);
            Console.WriteLine();

            ConvertMethodToRule(ruleWriter, method);
        }

        var rules = ruleWriter.ToString();
        Console.WriteLine(rules);
        TextCopy.Clipboard.SetText(rules);
    }

    static string RuleFormat => @"
rule(""{0}"")
{{
{1}
{2}
{3}
}}";

    static string EventFormat => @"
    event
    {{
        {0};
    }}";

    static string ConditionsFormat => @"
    conditions
    {{
        {0};
    }}";

    static string ActionsFormat => @"
    actions
    {{
{0}
    }}";

    static void ConvertMethodToRule(StringWriter ruleWriter, MethodDefinition method)
    {
        var writer = new StringWriter();
        var writeLine = (Action<object>)(str => writer.WriteLine($"        {str}"));

        writeLine("// Header");
        foreach (var line in MethodHeaderActions(method))
            writeLine(line());
        writeLine("");

        writeLine("// Body");
        foreach (var instr in method.Body.Instructions)
        {
            writeLine($"// {instr}");
            foreach (var line in ToWorkshopActions(method, instr))
                writeLine(line());
        }

        ruleWriter.WriteLine(string.Format(
            RuleFormat,
            method.Name,
            string.Format(EventFormat, "Ongoing - Global"),
            method.Name == "TestWait" ? "" : string.Format(ConditionsFormat, "Global Variable(F) == 1"),
            string.Format(ActionsFormat, writer.ToString())));
    }
}
