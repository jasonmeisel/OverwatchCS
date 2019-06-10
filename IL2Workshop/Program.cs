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
    public static char CallStack => 'F';
    public static char CallStackIndex => 'G';

    public static char JumpOffsetStack => 'J';
    public static char JumpOffsetStackIndex => 'K';

    public static char LocalsStack => 'L';
    public static char LocalsStackIndex => 'M';

    public static char ParameterStack => 'P';
    public static char ParameterStackIndex => 'Q';

    public static char Temporary => 'T';

    public static char VariableStack => 'V';
    public static char VariableStackIndex => 'W';
}

public static class Actions
{
    public class Stack
    {
        public char stackVar;
        public char stackIndexVar;

        public LazyString[] Push(LazyString value)
        {
            return new[] {
                ArrayAppend(stackVar, value),
                SetGlobal(stackIndexVar, () => $"Add(1, {GetGlobal(stackIndexVar)()})"),
            };
        }

        public LazyString[] Pop(int count)
        {
            var newSize = Add(GetGlobal(stackIndexVar), () => (1 - count).ToString());
            return new[] {
                SetGlobal(stackVar, ArraySlice(GetGlobal(stackVar), () => "0", newSize)),
                SetGlobal(stackIndexVar, Subtract(newSize, () => "1")),
            };
        }

        public LazyString GetLastElement(int offset)
        {
            return ArraySubscript(GetGlobal(stackVar), Add(GetGlobal(stackIndexVar), () => (-offset).ToString()));
        }

        public IEnumerable<LazyString> SetLastElement(int offset, LazyString value)
        {
            var before = ArraySlice(
                GetGlobal(stackVar),
                () => "0",
                Subtract(GetGlobal(stackIndexVar), () => (offset).ToString()));
            var after = ArraySlice(
                GetGlobal(stackVar),
                Subtract(GetGlobal(stackIndexVar), () => (offset - 1).ToString()),
                () => (offset).ToString());

            yield return SetGlobal(stackVar, ArrayConcat(ArrayConcat(before, value), after));

            // yield return SetGlobal(Variables.Temporary, after);
            // yield return SetGlobal(stackVar, ArrayConcat(before, value));
            // yield return SetGlobal(stackVar, ArrayConcat(GetGlobal(stackVar), GetGlobal(Variables.Temporary)));
        }

        public LazyString[] Resize(LazyString size)
        {
            return new[] {
                ResizeArray(stackVar, size),
                SetGlobal(stackIndexVar, Add(size, () => "-1")),
            };
        }
    }

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

    public static LazyString ResizeArray(char stackVar, LazyString size)
    {
        return SetGlobal(stackVar, ArraySlice(GetGlobal(stackVar), () => "0", size));
    }

    public static LazyString CreateArray(int count)
    {
        if (count == 0)
            return () => "Empty Array";
        return ArrayConcat(CreateArray(count - 1), () => "0");
    }

    public static Stack VariableStack = new Stack { stackVar = Variables.VariableStack, stackIndexVar = Variables.VariableStackIndex };
    public static Stack ParameterStack = new Stack { stackVar = Variables.ParameterStack, stackIndexVar = Variables.ParameterStackIndex };
    public static Stack CallStack = new Stack { stackVar = Variables.CallStack, stackIndexVar = Variables.CallStackIndex };
    public static Stack JumpOffsetStack = new Stack { stackVar = Variables.JumpOffsetStack, stackIndexVar = Variables.JumpOffsetStackIndex };
    public static Stack LocalsStack = new Stack { stackVar = Variables.LocalsStack, stackIndexVar = Variables.LocalsStackIndex };

    public static LazyString Add(LazyString valueA, LazyString valueB)
    {
        return () => $"Add({valueA()}, {valueB()})";
    }

    public static LazyString Subtract(LazyString valueA, LazyString valueB)
    {
        return () => $"Subtract({valueA()}, {valueB()})";
    }

    public static LazyString Max(LazyString valueA, LazyString valueB)
    {
        return () => $"Max({valueA()}, {valueB()})";
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


class Transpiler
{
    IEnumerable<LazyString> MethodHeaderActions(MethodDefinition method)
    {
        var firstActions = new LazyString[]
        {
            () => "Abort If Condition Is False;",
            () => "Wait(0, Ignore Condition);",
            SetGlobal(Variables.Temporary, JumpOffsetStack.GetLastElement(0)),
        };

        var targets = method.Body.Instructions.
            // jumps targets or call returns
            Select(i => i.Operand as Instruction ?? (i.Operand is MethodDefinition ? i.Next : null)).
            Where(t => t != null).
            Distinct().
            ToArray();
        var numTargets = targets.Length;
        var targetJumps = targets.Select((target, index) => {
            var skip = SkipIf(
                Equal(GetGlobal(Variables.Temporary), GetJumpId(target)),
                () => (numTargets - index + CalcNumActionsToSkip(method, target)).ToString());
            return (LazyString)(() => $"{skip()}         // {target.ToString()}");
        });
        
        var headerActions = firstActions.Concat(JumpOffsetStack.Pop(1)).Concat(targetJumps).ToArray();
        if (!IsMethodMain(method))
            return headerActions;
        
        var mainActions = new[]
        {
            SetGlobal(JumpOffsetStack.stackVar, CreateArray(1)),
            SetGlobal(LocalsStack.stackVar, CreateArray(1)),
            SetGlobal(ParameterStack.stackVar, CreateArray(1)),
            SetGlobal(VariableStack.stackVar, CreateArray(1)),
        };
        var mainActionsCount = mainActions.Count();
        var skipMainActions = SkipIf(NotEqual(GetGlobal(JumpOffsetStack.stackVar), () => "0"), () => mainActionsCount.ToString());
        return new[] { skipMainActions }.Concat(mainActions).Concat(headerActions);
    }

    Dictionary<OpCode, ToWorkshopActionFunc> s_toWorkshopActionsDict;
    Dictionary<OpCode, ToWorkshopActionFunc> ToWorkshopActionsDict => s_toWorkshopActionsDict = s_toWorkshopActionsDict ?? CreateToWorkshopActionsDict();

    static LazyString GetJumpId(Instruction instruction) => () => (instruction.Offset + 1).ToString();

    Dictionary<OpCode, ToWorkshopActionFunc> CreateToWorkshopActionsDict()
    {
        var dict = new Dictionary<OpCode, ToWorkshopActionFunc>();

        dict[OpCodes.Ldarg_0] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Parameters.Count - 1));
        dict[OpCodes.Ldarg_1] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Parameters.Count - 2));
        dict[OpCodes.Ldarg_2] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Parameters.Count - 3));
        dict[OpCodes.Ldarg_3] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Parameters.Count - 4));

        dict[OpCodes.Add] = (method, instruction) => DoBinaryOp(Add);
        dict[OpCodes.Sub] = (method, instruction) => DoBinaryOp(Subtract);

        // TODO: pop parameters off stack
        dict[OpCodes.Ret] = (method, instruction) => CallStack.Pop(1).
            Concat(LocalsStack.Pop(GetNumLocalVariables(method))).
            Concat(new LazyString[]
                {
                    () => "Abort;"
                });

        // jump if true
        dict[OpCodes.Brtrue_S] = (method, instruction) =>
            new[] { SetGlobal(Variables.Temporary, VariableStack.GetLastElement(0)) }.
                Concat(VariableStack.Pop(1)).
                Concat(new[]
                {
                    SkipIf(Equal(GetGlobal(Variables.Temporary), () => "0"), () => "3"),
                }).
                Concat(JumpOffsetStack.Push(GetJumpId((Instruction)instruction.Operand))).
                Concat(new LazyString[] {
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
                Concat(VariableStack.Pop(2)).
                Concat(new[]
                {
                    SkipIf(Equal(ArraySubscript(GetGlobal(Variables.Temporary), () => "0"), ArraySubscript(GetGlobal(Variables.Temporary), () => "1")), () => "3"),
                }).
                Concat(JumpOffsetStack.Push(GetJumpId((Instruction)instruction.Operand))).
                Concat(new LazyString[] {
                    () => "Loop;",
                });

        // jump
        dict[OpCodes.Br_S] = (method, instruction) =>
            JumpOffsetStack.Push(GetJumpId((Instruction)instruction.Operand)).
            Concat(new LazyString[] {
                () => "Loop;",
            });

        // shouldn't need to convert
        dict[OpCodes.Conv_R4] = (method, instruction) => new LazyString[0];

        dict[OpCodes.Ldc_I4_0] = (method, instruction) => VariableStack.Push(() => "0");
        dict[OpCodes.Ldc_I4_1] = (method, instruction) => VariableStack.Push(() => "1");
        dict[OpCodes.Ldc_I4_S] = (method, instruction) => VariableStack.Push(() => ((System.SByte)instruction.Operand).ToString());
        dict[OpCodes.Ldc_R4] = (method, instruction) => VariableStack.Push(() => ((float)instruction.Operand).ToString());
        dict[OpCodes.Dup] = (method, instruction) => VariableStack.Push(VariableStack.GetLastElement(0));

        dict[OpCodes.Stloc_0] = (method, instruction) => Impl_Stloc(method, 0);
        dict[OpCodes.Stloc_1] = (method, instruction) => Impl_Stloc(method, 0);
        dict[OpCodes.Ldloc_0] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, 0)));
        dict[OpCodes.Ldloc_1] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, 1)));

        dict[OpCodes.Starg_S] = Impl_Starg_S;
        dict[OpCodes.Call] = Impl_Call;

        return dict;
    }

    private static IEnumerable<LazyString> Impl_Stloc(MethodDefinition method, int localVariableIndex)
    {
        return LocalsStack.SetLastElement(GetLocalVariableStackOffset(method, localVariableIndex), VariableStack.GetLastElement(0)).
            Concat(VariableStack.Pop(1));
    }

    private static int GetLocalVariableStackOffset(MethodDefinition method, int localVariableIndex)
    {
        return GetNumLocalVariables(method) - 1 + localVariableIndex;
    }

    static int GetNumLocalVariables(MethodDefinition method)
    {
        return method.Body.Variables.Count;
    }

    IEnumerable<LazyString> Impl_Call(MethodDefinition method, Instruction instruction)
    {
        if (instruction.Operand is MethodDefinition targetMethodDef)
            return Impl_Call_CustomMethod(method, instruction, targetMethodDef);
        if (instruction.Operand is MethodReference targetMethodRef)
            return Impl_Call_WorkshopAction(method, instruction, targetMethodRef);
        throw new ArgumentException();
    }

    IEnumerable<LazyString> Impl_Call_CustomMethod(MethodDefinition method, Instruction instruction, MethodDefinition targetMethod)
    {
        // pop the parameters off the variable stack and onto the parameter stack
        yield return ArrayAppend(Variables.ParameterStack, ArraySlice(
            GetGlobal(Variables.VariableStack),
            Subtract(GetGlobal(Variables.VariableStackIndex), () => (targetMethod.Parameters.Count - 1).ToString()),
            () => targetMethod.Parameters.Count.ToString()));
        yield return SetGlobal(Variables.ParameterStackIndex,
            Add(GetGlobal(Variables.ParameterStackIndex), () => targetMethod.Parameters.Count.ToString()));
        foreach (var action in VariableStack.Pop(targetMethod.Parameters.Count))
            yield return action;

        var functionId = GetFunctionId(targetMethod);
        foreach (var action in CallStack.Push(() => functionId.ToString()))
            yield return action;

        foreach (var action in JumpOffsetStack.Push(GetJumpId(instruction.Next)))
            yield return action;
        foreach (var action in JumpOffsetStack.Push(() => "0"))
            yield return action;
        
        // TODO: optimize this
        foreach (var i in Enumerable.Range(0, GetNumLocalVariables(targetMethod)))
            foreach (var action in LocalsStack.Push(() => "0"))
                yield return action;

        yield return () => "Abort;";
    }

    static IEnumerable<LazyString> Impl_Call_WorkshopAction(MethodDefinition method, Instruction instruction, MethodReference targetMethodRef)
    {
        // TODO: assert that the method reference is a workshop action

        switch (targetMethodRef.Name)
        {
            case "Wait":
                yield return () => $"Wait({VariableStack.GetLastElement(0)()}, Ignore Condition);";
                break;
            case "DebugLog":
                yield return () => $"Big Message(All Players(All Teams), String(\"({{0}})\", {VariableStack.GetLastElement(0)()}, Null, Null));";
                yield return () => "Wait(0, Ignore Condition);";
                break;
            default:
                throw new ArgumentException();
        }

        foreach (var action in VariableStack.Pop(targetMethodRef.Parameters.Count))
            yield return action;
    }

    IEnumerable<LazyString> ToWorkshopActions(MethodDefinition method, Instruction instruction)
    {
        if (ToWorkshopActionsDict.TryGetValue(instruction.OpCode, out var toActions))
            return toActions(method, instruction);
        throw new Exception($"Unsupported opcode {instruction.OpCode}");
    }

    private static IEnumerable<LazyString> Impl_Starg_S(MethodDefinition method, Instruction instruction)
    {
        yield return SetGlobal(Variables.Temporary, ArraySlice(
            GetGlobal(Variables.ParameterStack),
            Subtract(GetGlobal(Variables.ParameterStackIndex), () => (method.Parameters.Count - 1).ToString()),
            () => method.Parameters.Count.ToString()));

        foreach (var action in ParameterStack.Pop(method.Parameters.Count))
            yield return action;

        foreach (var i in Enumerable.Range(0, method.Parameters.Count))
        {
            var push = ParameterStack.Push(
                instruction.Operand == method.Parameters[i] ?
                VariableStack.GetLastElement(0) :
                ArraySubscript(GetGlobal(Variables.Temporary), i));
            foreach (var action in push)
                yield return action;
        }

        foreach (var action in VariableStack.Pop(1))
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
            VariableStack.Pop(2)
        ).Concat(
            // push the addition of them onto the stack
            VariableStack.Push(binaryOp(ArraySubscript(GetGlobal(Variables.Temporary), 0), ArraySubscript(GetGlobal(Variables.Temporary), 1)))
        ).ToArray();
    }

    int CalcNumActionsToSkip(MethodDefinition method, Instruction target)
    {
        return method.Body.Instructions.TakeWhile(i => i != target).Sum(i => ToWorkshopActions(method, i).Count()) - 1;
    }

    Dictionary<MethodDefinition, int> m_functionIds;
    int GetFunctionId(MethodDefinition method)
    {
        if (IsMethodMain(method))
            return 0;
        return m_functionIds[method];
    }

    public string TranspileToRules(string source)
    {
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
            throw new System.Exception("Fail!");
        }

        var module = ModuleDefinition.ReadModule("AsmBuild.dll");
        var methods = module.GetType("MainClass").Methods;
        GenerateFunctionIds(methods);

        var ruleWriter = new StringWriter();
        foreach (var method in methods)
        {
            foreach (var instr in method.Body.Instructions)
                Console.WriteLine(instr);
            Console.WriteLine();

            ConvertMethodToRule(ruleWriter, method);
        }

        return ruleWriter.ToString();
    }

    void GenerateFunctionIds(Mono.Collections.Generic.Collection<MethodDefinition> methods)
    {
        // start at 1 so that 0 doesn't set any functions off
        m_functionIds = methods.Zip(Enumerable.Range(1, methods.Count), (m, i) => (m, i)).ToDictionary(pair => pair.m, pair => pair.i);
    }

    static void Main(string[] args)
    {
        var transpiler = new Transpiler();
        var source = File.ReadAllText("Test\\Test.cs");
        var rules = transpiler.TranspileToRules(source);
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

    void ConvertMethodToRule(StringWriter ruleWriter, MethodDefinition method)
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
            writeLine($"    // {instr}");
            foreach (var line in ToWorkshopActions(method, instr))
                writeLine(line());
        }

        ruleWriter.WriteLine(string.Format(
            RuleFormat,
            method.Name,
            string.Format(EventFormat, "Ongoing - Global"),
            string.Format(ConditionsFormat, $"{CallStack.GetLastElement(0)()} == {GetFunctionId(method)}"),
            string.Format(ActionsFormat, writer.ToString())));
    }

    private static bool IsMethodMain(MethodDefinition method)
    {
        return method.Name == "Main";
    }
}
