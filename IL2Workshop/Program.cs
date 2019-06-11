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
using ToWorkshopActionFunc = System.Func<MethodInfo, Mono.Cecil.Cil.Instruction, System.Collections.Generic.IEnumerable<System.Func<string>>>;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class Extensions
{
    public static string ListToString<T>(this IEnumerable<T> list, string separator = ", ")
    {
        return string.Join(separator, list);
    }

    public static IEnumerable<T> Interleave<T>(this IEnumerable<T> list, IEnumerable<T> list2)
    {
        var enum1 = list.GetEnumerator();
        var enum2 = list2.GetEnumerator();
        while (true)
        {
            if (enum1.MoveNext())
                yield return enum1.Current;
            else
                yield break;

            if (enum2.MoveNext())
                yield return enum2.Current;
            else
                yield break;
        }
    }
}

class MethodInfo
{
    public MethodDefinition Definition;
    public List<Instruction> Instructions;
}

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

    public static LazyString ArrayLast(LazyString array)
    {
        return () => $"Last Of({array()})";
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
    public static LazyString Mul(LazyString valueA, LazyString valueB)
    {
        return () => $"Multiply({valueA()}, {valueB()})";
    }

    public static LazyString Div(LazyString valueA, LazyString valueB)
    {
        return () => $"Divide({valueA()}, {valueB()})";
    }

    public static LazyString Mod(LazyString valueA, LazyString valueB)
    {
        return () => $"Modulo({valueA()}, {valueB()})";
    }

    public static LazyString And(LazyString valueA, LazyString valueB)
    {
        return () => $"And({valueA()}, {valueB()})";
    }

    public static LazyString Or(LazyString valueA, LazyString valueB)
    {
        return () => $"Or({valueA()}, {valueB()})";
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
    IEnumerable<LazyString> MethodHeaderActions(MethodInfo method)
    {
        var firstActions = new LazyString[]
        {
            () => $"Abort If (Not({FunctionCondition(method)()}));",
            () => "Wait(0, Abort When False);",
            SetGlobal(Variables.Temporary, JumpOffsetStack.GetLastElement(0)),
        };

        var targets = method.Instructions.
            // jumps targets or call returns
            Select(i => i.Operand as Instruction ?? (i.Operand is MethodDefinition ? i.Next : null)).
            Where(t => t != null && t.Offset != 0).
            Distinct().
            ToArray();
        var numTargets = targets.Length;
        var targetJumps = targets.Select((target, index) =>
        {
            var skip = SkipIf(
                Equal(GetGlobal(Variables.Temporary), GetJumpId(target)),
                () => (numTargets - index + CalcNumActionsToSkip(method, target)).ToString());
            return (LazyString)(() => $"{skip()}         // {target.ToString()}");
        });

        var headerActions = firstActions.Concat(JumpOffsetStack.Pop(1)).Concat(targetJumps).ToArray();
        if (!IsMethodMain(method.Definition))
            return headerActions;

        var mainActions = new[]
        {
            SetGlobal(JumpOffsetStack.stackVar, CreateArray(1)),
            SetGlobal(LocalsStack.stackVar, CreateArray(1)),
            SetGlobal(ParameterStack.stackVar, CreateArray(1)),
            SetGlobal(VariableStack.stackVar, CreateArray(1)),
        }.Concat(CreateLocals(method.Definition)).ToArray();
        var mainActionsCount = mainActions.Count();
        var skipMainActions = SkipIf(NotEqual(GetGlobal(JumpOffsetStack.stackVar), () => "0"), () => mainActionsCount.ToString());
        return new[] { skipMainActions }.Concat(mainActions).Concat(headerActions);
    }

    Dictionary<OpCode, ToWorkshopActionFunc> s_toWorkshopActionsDict;
    Dictionary<OpCode, ToWorkshopActionFunc> ToWorkshopActionsDict => s_toWorkshopActionsDict = s_toWorkshopActionsDict ?? CreateToWorkshopActionsDict();

    static LazyString GetJumpId(Instruction instruction) => () => instruction.Offset.ToString();

    Dictionary<OpCode, ToWorkshopActionFunc> CreateToWorkshopActionsDict()
    {
        var dict = new Dictionary<OpCode, ToWorkshopActionFunc>();

        dict[OpCodes.Nop] = Impl_Nop;
        dict[OpCodes.Ldlen] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelema] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_I1] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_U1] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_I2] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_U2] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_I4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_U4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_I] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_R4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_R8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_Ref] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_I] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_I1] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_I2] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_I4] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_R4] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_R8] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_Ref] = Impl_UnimplementedOp;
        dict[OpCodes.Ldelem_Any] = Impl_UnimplementedOp;
        dict[OpCodes.Stelem_Any] = Impl_UnimplementedOp;
        dict[OpCodes.Newarr] = Impl_UnimplementedOp;
        dict[OpCodes.Box] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Cpobj] = Impl_UnimplementedOp;
        dict[OpCodes.Ldobj] = Impl_UnimplementedOp;
        dict[OpCodes.Ldstr] = Impl_UnimplementedOp;
        dict[OpCodes.Newobj] = Impl_UnimplementedOp;
        dict[OpCodes.Castclass] = Impl_UnimplementedOp;
        dict[OpCodes.Isinst] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_R_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Unbox] = Impl_UnimplementedOp;
        dict[OpCodes.Throw] = Impl_UnimplementedOp;
        dict[OpCodes.Ldfld] = Impl_UnimplementedOp;
        dict[OpCodes.Ldflda] = Impl_UnimplementedOp;
        dict[OpCodes.Unbox_Any] = Impl_UnimplementedOp;
        dict[OpCodes.Stfld] = Impl_UnimplementedOp;
        dict[OpCodes.Ldsflda] = Impl_UnimplementedOp;
        dict[OpCodes.Stsfld] = Impl_UnimplementedOp;
        dict[OpCodes.Stobj] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I1_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I2_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I4_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I8_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U1_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U2_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U4_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U8_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Ldsfld] = Impl_UnimplementedOp;
        dict[OpCodes.Callvirt] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I1] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I2] = Impl_UnimplementedOp;
        dict[OpCodes.Cgt_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Clt] = Impl_UnimplementedOp;
        dict[OpCodes.Clt_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Ldftn] = Impl_UnimplementedOp;
        dict[OpCodes.Ldvirtftn] = Impl_UnimplementedOp;
        dict[OpCodes.Ldarg] = Impl_UnimplementedOp;
        dict[OpCodes.Ldarga] = Impl_UnimplementedOp;
        dict[OpCodes.Starg] = Impl_UnimplementedOp;
        dict[OpCodes.Ldloc] = Impl_UnimplementedOp;
        dict[OpCodes.Ldloca] = Impl_UnimplementedOp;
        dict[OpCodes.Stloc] = Impl_UnimplementedOp;
        dict[OpCodes.Localloc] = Impl_UnimplementedOp;
        dict[OpCodes.Endfilter] = Impl_UnimplementedOp;
        dict[OpCodes.Unaligned] = Impl_UnimplementedOp;
        dict[OpCodes.Volatile] = Impl_UnimplementedOp;
        dict[OpCodes.Tail] = Impl_UnimplementedOp;
        dict[OpCodes.Initobj] = Impl_UnimplementedOp;
        dict[OpCodes.Constrained] = Impl_UnimplementedOp;
        dict[OpCodes.Cpblk] = Impl_UnimplementedOp;
        dict[OpCodes.Initblk] = Impl_UnimplementedOp;
        dict[OpCodes.No] = Impl_UnimplementedOp;
        dict[OpCodes.Rethrow] = Impl_UnimplementedOp;
        dict[OpCodes.Sizeof] = Impl_UnimplementedOp;
        dict[OpCodes.Cgt] = Impl_UnimplementedOp;
        dict[OpCodes.Ceq] = Impl_UnimplementedOp;
        dict[OpCodes.Arglist] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_U] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U2] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I4] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U4] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U8] = Impl_UnimplementedOp;
        dict[OpCodes.Refanyval] = Impl_UnimplementedOp;
        dict[OpCodes.Ckfinite] = Impl_UnimplementedOp;
        dict[OpCodes.Mkrefany] = Impl_UnimplementedOp;
        dict[OpCodes.Ldtoken] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_U2] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_U1] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U1] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_I] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U] = Impl_UnimplementedOp;
        dict[OpCodes.Add_Ovf] = Impl_UnimplementedOp;
        dict[OpCodes.Add_Ovf_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Mul_Ovf] = Impl_UnimplementedOp;
        dict[OpCodes.Mul_Ovf_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Sub_Ovf] = Impl_UnimplementedOp;
        dict[OpCodes.Sub_Ovf_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Endfinally] = Impl_UnimplementedOp;
        dict[OpCodes.Leave] = Impl_UnimplementedOp;
        dict[OpCodes.Leave_S] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_I] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I] = Impl_UnimplementedOp;
        dict[OpCodes.Refanytype] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_U8] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_R8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_7] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_S] = (method, instruction) => VariableStack.Push(() => ((System.SByte)instruction.Operand).ToString());
        dict[OpCodes.Ldc_I4] = (method, instruction) => VariableStack.Push(() => ((int)instruction.Operand).ToString());
        dict[OpCodes.Ldc_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_R4] = (method, instruction) => VariableStack.Push(() => ((float)instruction.Operand).ToString());
        dict[OpCodes.Ldc_R8] = (method, instruction) => VariableStack.Push(() => ((double)instruction.Operand).ToString());
        dict[OpCodes.Dup] = (method, instruction) => VariableStack.Push(VariableStack.GetLastElement(0));
        dict[OpCodes.Pop] = (method, instruction) => VariableStack.Pop(1);
        dict[OpCodes.Jmp] = Impl_UnimplementedOp;
        dict[OpCodes.Call] = Impl_Call;
        dict[OpCodes.Calli] = Impl_UnimplementedOp;

        dict[OpCodes.Ret] = (method, instruction) => CallStack.Pop(1).
            Concat(LocalsStack.Pop(GetNumLocalVariables(method.Definition))).
            Concat(ParameterStack.Pop(method.Definition.Parameters.Count)).
            Concat(new LazyString[]
                {
                    () => "Loop;"
                });

        dict[OpCodes.Br_S] = (method, instruction) =>
            JumpOffsetStack.Push(GetJumpId((Instruction)instruction.Operand)).
            Concat(new LazyString[] {
                () => "Loop;",
            });

        dict[OpCodes.Brfalse_S] = Impl_UnimplementedOp;

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
        dict[OpCodes.Beq_S] = Impl_UnimplementedOp;
        dict[OpCodes.Bge_S] = Impl_UnimplementedOp;
        dict[OpCodes.Bgt_S] = Impl_UnimplementedOp;
        dict[OpCodes.Ble_S] = Impl_UnimplementedOp;
        dict[OpCodes.Blt_S] = Impl_UnimplementedOp;
        dict[OpCodes.Bne_Un_S] = Impl_UnimplementedOp;

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
        dict[OpCodes.Bge_Un_S] = Impl_UnimplementedOp;

        dict[OpCodes.Ldc_I4_6] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_5] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_3] = Impl_UnimplementedOp;
        dict[OpCodes.Break] = Impl_UnimplementedOp;

        dict[OpCodes.Ldarg_0] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Definition.Parameters.Count - 1));
        dict[OpCodes.Ldarg_1] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Definition.Parameters.Count - 2));
        dict[OpCodes.Ldarg_2] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Definition.Parameters.Count - 3));
        dict[OpCodes.Ldarg_3] = (method, instruction) =>
            VariableStack.Push(ParameterStack.GetLastElement(method.Definition.Parameters.Count - 4));

        dict[OpCodes.Ldloc_0] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, 0)));
        dict[OpCodes.Ldloc_1] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, 1)));
        dict[OpCodes.Ldloc_2] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, 2)));
        dict[OpCodes.Ldloc_3] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, 3)));
        dict[OpCodes.Stloc_0] = (method, instruction) => Impl_Stloc(method, 0);
        dict[OpCodes.Stloc_1] = (method, instruction) => Impl_Stloc(method, 1);
        dict[OpCodes.Bgt_Un_S] = Impl_UnimplementedOp;
        dict[OpCodes.Stloc_2] = (method, instruction) => Impl_Stloc(method, 2);
        dict[OpCodes.Ldarg_S] = Impl_UnimplementedOp;
        dict[OpCodes.Ldarga_S] = Impl_UnimplementedOp;
        dict[OpCodes.Starg_S] = Impl_Starg_S;
        dict[OpCodes.Ldloc_S] = Impl_UnimplementedOp;
        dict[OpCodes.Ldloca_S] = Impl_UnimplementedOp;
        dict[OpCodes.Stloc_S] = Impl_UnimplementedOp;
        dict[OpCodes.Ldnull] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_M1] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_0] = (method, instruction) => VariableStack.Push(() => "0");
        dict[OpCodes.Ldc_I4_1] = (method, instruction) => VariableStack.Push(() => "1");
        dict[OpCodes.Ldc_I4_2] = (method, Instruction) => VariableStack.Push(() => "2");
        dict[OpCodes.Stloc_3] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_U4] = Impl_UnimplementedOp;
        dict[OpCodes.Ble_Un_S] = Impl_UnimplementedOp;
        dict[OpCodes.Br] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_R4] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_R8] = Impl_UnimplementedOp;

        dict[OpCodes.Add] = (method, instruction) => DoBinaryOp(Add);
        dict[OpCodes.Sub] = (method, instruction) => DoBinaryOp(Subtract);
        dict[OpCodes.Mul] = (method, instruction) => DoBinaryOp(Mul);
        dict[OpCodes.Div] = (method, instruction) => DoBinaryOp(Div);
        dict[OpCodes.Div_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Rem] = (method, instruction) => DoBinaryOp(Mod);
        dict[OpCodes.Rem_Un] = Impl_UnimplementedOp;
        dict[OpCodes.And] = (method, instruction) => DoBinaryOp(And);
        dict[OpCodes.Or] = (method, instruction) => DoBinaryOp(Or);
        dict[OpCodes.Xor] = Impl_UnimplementedOp;
        dict[OpCodes.Shl] = Impl_UnimplementedOp;
        dict[OpCodes.Shr] = Impl_UnimplementedOp;
        dict[OpCodes.Shr_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Neg] = Impl_UnimplementedOp;
        dict[OpCodes.Not] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_I1] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_I2] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_I4] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_R4] = Impl_Nop; // shouldn't need to convert int to real
        dict[OpCodes.Stind_I4] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_I2] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_I1] = Impl_UnimplementedOp;
        dict[OpCodes.Stind_Ref] = Impl_UnimplementedOp;
        dict[OpCodes.Brfalse] = Impl_UnimplementedOp;
        dict[OpCodes.Brtrue] = Impl_UnimplementedOp;
        dict[OpCodes.Beq] = Impl_UnimplementedOp;
        dict[OpCodes.Bge] = Impl_UnimplementedOp;
        dict[OpCodes.Bgt] = Impl_UnimplementedOp;
        dict[OpCodes.Ble] = Impl_UnimplementedOp;
        dict[OpCodes.Blt] = Impl_UnimplementedOp;
        dict[OpCodes.Bne_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Bge_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Bgt_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Ble_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Blt_Un_S] = Impl_UnimplementedOp;
        dict[OpCodes.Blt_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_I1] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_U1] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_I2] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_U2] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_I4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_U4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_I8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_I] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_R4] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_R8] = Impl_UnimplementedOp;
        dict[OpCodes.Ldind_Ref] = Impl_UnimplementedOp;
        dict[OpCodes.Switch] = Impl_UnimplementedOp;
        dict[OpCodes.Readonly] = Impl_UnimplementedOp;

        return dict;
    }

    static readonly LazyString[] EmptyLazyStringArray = new LazyString[0];
    static LazyString[] Impl_Nop(MethodInfo method, Instruction instruction)
    {
        return EmptyLazyStringArray;
    }

    static LazyString[] Impl_UnimplementedOp(MethodInfo method, Instruction instruction)
    {
        throw new NotImplementedException(instruction.OpCode.ToString());
    }

    private static IEnumerable<LazyString> Impl_Stloc(MethodInfo method, int localVariableIndex)
    {
        return LocalsStack.SetLastElement(GetLocalVariableStackOffset(method, localVariableIndex), VariableStack.GetLastElement(0)).
            Concat(VariableStack.Pop(1));
    }

    private static int GetLocalVariableStackOffset(MethodInfo method, int localVariableIndex)
    {
        return GetNumLocalVariables(method.Definition) - localVariableIndex - 1;
    }

    static int GetNumLocalVariables(MethodDefinition method)
    {
        return method.Body.Variables.Count;
    }

    IEnumerable<LazyString> Impl_Call(MethodInfo method, Instruction instruction)
    {
        if (instruction.Operand is MethodDefinition targetMethodDef)
        {
            if (targetMethodDef.DeclaringType.Name == "Generated")
                return Impl_Call_WorkshopAction(method, instruction, targetMethodDef, m_generatedMethodToWorkshopCode[targetMethodDef.Name]);
            return Impl_Call_CustomMethod(method, instruction, targetMethodDef);
        }
        if (instruction.Operand is MethodReference targetMethodRef)
            return Impl_Call_WorkshopAction(method, instruction, targetMethodRef);
        throw new ArgumentException();
    }

    IEnumerable<LazyString> Impl_Call_CustomMethod(MethodInfo method, Instruction instruction, MethodDefinition targetMethod)
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

        foreach (var action in CreateLocals(targetMethod))
            yield return action;

        yield return () => "Loop;";
    }

    static IEnumerable<LazyString> CreateLocals(MethodDefinition targetMethod)
    {
        // TODO: optimize this
        foreach (var i in Enumerable.Range(0, GetNumLocalVariables(targetMethod)))
            foreach (var action in LocalsStack.Push(() => "0"))
                yield return action;
    }

    static IEnumerable<LazyString> Impl_Call_WorkshopAction(MethodInfo method, Instruction instruction, MethodReference targetMethodRef, string codeOverride = null)
    {
        // TODO: assert that the method reference is a workshop action

        var hasReturnInTemp = false;

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
                var targetMethod = GetWorkshopMethod(targetMethodRef);
                var code = codeOverride ?? GetCodeName(targetMethod);
                if (code == null)
                    throw new ArgumentException();

                var parameters = targetMethodRef.Parameters;
                var paramList = parameters.Select((p, i) => VariableStack.GetLastElement(parameters.Count - i - 1)());
                if (parameters.Any())
                    if (code.Contains("<PARAM>"))
                        code = code.Split("<PARAM>").Interleave(paramList).ListToString("");
                    else
                        code = $"{code}({paramList.ListToString()})";

                if (targetMethodRef.ReturnType.Name != "Void")
                {
                    yield return SetGlobal(Variables.Temporary, () => code);
                    hasReturnInTemp = true;
                }
                else
                    yield return () => code + ";";
                break;
        }

        foreach (var action in VariableStack.Pop(targetMethodRef.Parameters.Count))
            yield return action;

        if (hasReturnInTemp)
            foreach (var action in VariableStack.Push(GetGlobal(Variables.Temporary)))
                yield return action;
    }

    private static string GetCodeName(System.Reflection.MethodInfo targetMethod)
    {
        var attributes = targetMethod?.GetCustomAttributes(typeof(WorkshopCodeName), true);
        return (attributes?.FirstOrDefault() as WorkshopCodeName)?.Name;
    }

    private static string GetCodeName(IMethodSymbol targetMethod)
    {
        return targetMethod.GetAttributes().
            FirstOrDefault(attr => attr.AttributeClass.Name == typeof(WorkshopCodeName).Name)?.
            ConstructorArguments.FirstOrDefault().Value as string;
    }

    static System.Reflection.MethodInfo GetWorkshopMethod(MethodReference targetMethodRef)
    {
        if (targetMethodRef == null)
            return null;
        // var parameters = targetMethodRef.Parameters.Select(p => GetWorkshopType(p.ParameterType.FullName)).ToArray();
        return GetWorkshopType(targetMethodRef.DeclaringType.FullName)?.
            GetMethods().FirstOrDefault(m => targetMethodRef.Name == m.Name);
    }

    static Type GetWorkshopType(string type)
    {
        return typeof(Workshop.Values).Assembly.GetType(type);
    }

    IEnumerable<LazyString> ToWorkshopActions(MethodInfo method, Instruction instruction)
    {
        if (ToWorkshopActionsDict.TryGetValue(instruction.OpCode, out var toActions))
            return toActions(method, instruction);
        throw new Exception($"Unsupported opcode {instruction.OpCode}");
    }

    private static IEnumerable<LazyString> Impl_Starg_S(MethodInfo method, Instruction instruction)
    {
        var parameters = method.Definition.Parameters;
        var paramCount = parameters.Count;

        yield return SetGlobal(Variables.Temporary, ArraySlice(
            GetGlobal(Variables.ParameterStack),
            Subtract(GetGlobal(Variables.ParameterStackIndex), () => (paramCount - 1).ToString()),
            () => paramCount.ToString()));

        foreach (var action in ParameterStack.Pop(paramCount))
            yield return action;

        foreach (var i in Enumerable.Range(0, paramCount))
        {
            var push = ParameterStack.Push(
                instruction.Operand == parameters[i] ?
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

    int CalcNumActionsToSkip(MethodInfo method, Instruction target)
    {
        return method.Instructions.TakeWhile(i => i != target).Sum(i => ToWorkshopActions(method, i).Count()) - 1;
    }

    Dictionary<MethodDefinition, int> m_functionIds;
    int GetFunctionId(MethodDefinition method)
    {
        if (IsMethodMain(method))
            return 0;
        return m_functionIds[method];
    }

    Dictionary<string, string> m_generatedMethodToWorkshopCode = new Dictionary<string, string>();

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
        var syntaxTree = SyntaxFactory.ParseSyntaxTree(source);
        var compilation = CSharpCompilation.Create(
            "AsmBuild",
            new[] { syntaxTree },
            references,
            options);
        var semanticModel = compilation.GetSemanticModel(syntaxTree);

        var substituter = new MethodSubstituter(semanticModel, m_generatedMethodToWorkshopCode);
        var newRoot = substituter.Visit(syntaxTree.GetRoot());
        var newTree = SyntaxFactory.SyntaxTree(newRoot);

        compilation = compilation.RemoveAllSyntaxTrees();
        compilation = compilation.AddSyntaxTrees(newTree, substituter.GetGeneratedClass());

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
            var methodInfo = new MethodInfo
            {
                Definition = method,
                Instructions = method.Body.Instructions.ToList(),
            };

            // TODO: add analyzer to prevent storing un-storable variables on stack (Array, Player, etc)

            foreach (var instr in methodInfo.Instructions)
                Console.WriteLine(instr);
            Console.WriteLine();

            ConvertMethodToRule(ruleWriter, methodInfo);
        }

        return ruleWriter.ToString();
    }

    class MethodSubstituter : CSharpSyntaxRewriter
    {
        int m_methodCount = 0;
        StringWriter m_methodsSource = new StringWriter();
        SemanticModel m_semanticModel;
        Dictionary<string, string> m_generatedMethodToWorkshopCode;

        public MethodSubstituter(SemanticModel semanticModel, Dictionary<string, string> generatedMethodToWorkshopCode)
        {
            m_semanticModel = semanticModel;
            m_generatedMethodToWorkshopCode = generatedMethodToWorkshopCode;
        }

        public SyntaxTree GetGeneratedClass()
        {
            return SyntaxFactory.ParseSyntaxTree($"internal static class Generated {{ {m_methodsSource.ToString()} }}");
        }

        (string code, string[] paramTypes, ArgumentSyntax[] arguments) InvocationToWorkshopCode(InvocationExpressionSyntax invocation)
        {
            var argumentList = invocation.ArgumentList;

            var targetMethod = GetMethodSemantics(invocation);
            var targetCode = GetCodeName(targetMethod);
            if (targetCode != null)
            {
                var arguments = argumentList.Arguments.ToArray();

                // test for extension method
                // TODO: support struct methods?
                var isStatic = targetMethod.IsStatic;
                if (!isStatic && invocation.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    arguments = new[] { SyntaxFactory.Argument(memberAccess.Expression) }.
                        Concat(arguments).
                        ToArray();
                }

                if (arguments.Count() != 0)
                {
                    // TODO: add baking in literals
                    var methodSemantics = (Microsoft.CodeAnalysis.IMethodSymbol)m_semanticModel.GetSymbolInfo(invocation).Symbol;
                    var argCodes = arguments.Zip(methodSemantics.Parameters, (a, p) =>
                        a.Expression is InvocationExpressionSyntax i ?
                            InvocationToWorkshopCode(i) :
                            (
                                code: "<PARAM>",
                                paramTypes: new[] { p.Type.ToString() },
                                arguments: new[] { a }
                            )).
                        ToArray();

                    return (
                        $"{targetCode}({argCodes.Select(a => a.code).ListToString()})",
                        argCodes.SelectMany(a => a.paramTypes).ToArray(),
                        argCodes.SelectMany(a => a.arguments).ToArray()
                    );
                }
                return (targetCode, new string[0], new ArgumentSyntax[0]);
            }
            throw null;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax invocation)
        {
            var argumentList = invocation.ArgumentList;

            var targetMethod = GetMethodSemantics(invocation);
            var targetCode = GetCodeName(targetMethod);
            if (targetCode != null)
            {
                var baseMethodName = string.Join("", targetMethod.Name.Where(ch => char.IsLetterOrDigit(ch)));
                var generatedMethodName = $"Impl_{baseMethodName}_{m_methodCount++}";
                var workshopCode = InvocationToWorkshopCode(invocation);
                m_generatedMethodToWorkshopCode[generatedMethodName] = workshopCode.code;

                var methodSemantics = (Microsoft.CodeAnalysis.IMethodSymbol)m_semanticModel.GetSymbolInfo(invocation).Symbol;
                var returnType = methodSemantics.ReturnType;
                var paramListSource = workshopCode.paramTypes.Select((type, i) => $"{type} arg{i}").ListToString();
                var methodSource = $"internal static {returnType} {generatedMethodName} ({paramListSource}) => throw null;";
                m_methodsSource.WriteLine(methodSource);
                m_methodsSource.WriteLine();

                Console.WriteLine(methodSource);

                var arguments = SyntaxFactory.SeparatedList(workshopCode.arguments.Select(a => base.Visit(a)));
                return SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Generated"),
                        SyntaxFactory.IdentifierName(generatedMethodName)),
                    SyntaxFactory.ArgumentList(arguments));
            }

            return base.VisitInvocationExpression(invocation);
        }

        IMethodSymbol GetMethodSemantics(InvocationExpressionSyntax invocation)
        {
            switch (invocation.Expression)
            {
                case MemberAccessExpressionSyntax memberAccess:
                    return (IMethodSymbol)m_semanticModel.GetSymbolInfo(memberAccess.Name).Symbol;
                default:
                    Console.WriteLine(invocation);
                    Console.WriteLine(invocation.Expression);
                    return m_semanticModel.GetSymbolInfo(invocation.Expression).Symbol as IMethodSymbol;
            }
        }
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

    void ConvertMethodToRule(StringWriter ruleWriter, MethodInfo method)
    {
        var writer = new StringWriter();
        var writeLine = (Action<object>)(str => writer.WriteLine($"        {str}"));

        writeLine("// Header");
        foreach (var line in MethodHeaderActions(method))
            writeLine(line());
        writeLine("");

        writeLine("// Body");
        foreach (var instr in method.Instructions)
        {
            writeLine($"    // {instr}");
            foreach (var line in ToWorkshopActions(method, instr))
                writeLine(line());
        }

        ruleWriter.WriteLine(string.Format(
            RuleFormat,
            method.Definition.Name,
            string.Format(EventFormat, "Ongoing - Global"),
            string.Format(ConditionsFormat, $"{FunctionCondition(method)()} == True"),
            string.Format(ActionsFormat, writer.ToString())));
    }

    private LazyString FunctionCondition(MethodInfo method)
    {
        return Equal(ArrayLast(GetGlobal(CallStack.stackVar)), () => GetFunctionId(method.Definition).ToString());
    }

    private static bool IsMethodMain(MethodDefinition method)
    {
        return method.Name == "Main";
    }
}
