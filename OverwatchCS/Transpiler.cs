﻿using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Mono.Cecil;
using System.Linq;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.IO;

using LazyString = System.Func<string>;
using static Actions;
using ToWorkshopActionFunc = System.Func<MethodInfo, Mono.Cecil.Cil.Instruction, System.Collections.Generic.IEnumerable<System.Func<string>>>;
using System.Diagnostics;

using Variables = Workshop.Globals;

class MethodInfo
{
    public MethodDefinition Definition;
    public List<Instruction> Instructions;
}

partial class Transpiler
{
    static Transpiler()
    {
        s_workshopCodeAttributesByCodeName = typeof(WorkshopCodeAttribute).Assembly.DefinedTypes.
            SelectMany(t => t.DeclaredMethods).
            Select(m => GetWorkshopCodeAttribute(m)).
            Where(attr => attr != null).
            ToLookup(attr => attr.Name, attr => attr);
    }
    
    IEnumerable<LazyString> MethodHeaderActions(MethodInfo method)
    {
        var firstActions = new LazyString[]
        {
            () => $"Abort If (Not({FunctionCondition(method)()}));",
            () => "Wait(0, Abort When False);",
            SetGlobal(Variables.Temporary, JumpOffsetStack.GetLastElement(0)),
        };

        LazyString actionCountStr = () =>
        {
            var instruction = method.Instructions.First();
            var nextInChain = FindNextSkipChain(method, instruction);
            var actionCount = nextInChain == null ? 99999999 : CalcNumActionsToSkip(method, nextInChain) - CalcNumActionsToSkip(method, instruction);
            return actionCount.ToString();
        };

        var jumpToTarget = new[]
        {
            SetGlobal(Variables.Temporary2, GetGlobal(Variables.Temporary)),
            SetGlobal(Variables.Temporary, Subtract(GetGlobal(Variables.Temporary), Add(actionCountStr, () => "4"))),
            SkipIf(
                NotEqual(GetGlobal(Variables.Temporary2), () => "0"),
                Min(GetGlobal(Variables.Temporary2), Add(actionCountStr, () => "1"))),
        };

        return firstActions.Concat(JumpOffsetStack.Pop(1)).Concat(jumpToTarget);
    }

    Dictionary<OpCode, ToWorkshopActionFunc> s_toWorkshopActionsDict;
    Dictionary<OpCode, ToWorkshopActionFunc> ToWorkshopActionsDict => s_toWorkshopActionsDict = s_toWorkshopActionsDict ?? CreateToWorkshopActionsDict();

    Dictionary<FieldDefinition, int> m_staticFieldToIndex = new Dictionary<FieldDefinition, int>();

    LazyString GetJumpId(MethodInfo method, Instruction target) => () => CalcNumActionsToSkip(method, target).ToString();

    Dictionary<OpCode, ToWorkshopActionFunc> CreateToWorkshopActionsDict()
    {
        var dict = new Dictionary<OpCode, ToWorkshopActionFunc>();

        // default opcode is for the skip chain (see InsertSkipChainInstructions)
        dict[OpCodes.No] = Impl_SkipChainActions;
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
        dict[OpCodes.Stsfld] = (method, instruction) => new[]
        {
            SetGlobalAtIndex(Variables.StaticFields, GetStaticFieldIndex(instruction.Operand), VariableStack.GetLastElement(0))
        }.Concat(VariableStack.Pop(1));

        dict[OpCodes.Stobj] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I1_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I2_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I4_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_I8_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U1_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U2_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U4_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Conv_Ovf_U8_Un] = Impl_UnimplementedOp;

        dict[OpCodes.Ldsfld] = (method, instruction) =>
        {
            var index = GetStaticFieldIndex(instruction.Operand);
            return VariableStack.Push(ArraySubscript(GetGlobal(Variables.StaticFields), () => index.ToString()));
        };

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
            // if it's a workshop event, then it needs the max num of local variables (since it's called at runtime)
            Concat(LocalsStack.Pop(GetCustomAttributeData<WorkshopEventAttribute>(method) != null ? m_maxNumLocalVariables : GetNumLocalVariables(method.Definition))).
            Concat(ParameterStack.Pop(method.Definition.Parameters.Count)).
            // (loop instead of abort so you can call functions recursively)
            Concat(new LazyString[] { () => "Loop;" });

        dict[OpCodes.Br_S] = (method, instruction) => Impl_Jump_If(method, instruction, 0, () => "True");

        dict[OpCodes.Brfalse_S] = (method, instruction) => Impl_Jump_If(method, instruction, 1, Equal(ArraySubscript(GetGlobal(Variables.Temporary), () => "0"), () => "0"));
        dict[OpCodes.Brtrue_S] = (method, instruction) => Impl_Jump_If(method, instruction, 1, NotEqual(ArraySubscript(GetGlobal(Variables.Temporary), () => "0"), () => "0"));

        dict[OpCodes.Beq_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, "==");
        dict[OpCodes.Bge_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, ">=");
        dict[OpCodes.Bgt_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, ">");
        dict[OpCodes.Ble_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, "<=");
        dict[OpCodes.Blt_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, "<");

        dict[OpCodes.Bne_Un_S] = (method, instruction) => Impl_Jump_If(method, instruction, NotEqual);
        dict[OpCodes.Bge_Un_S] = dict[OpCodes.Bge_S];

        dict[OpCodes.Ldc_I4_6] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_5] = (method, Instruction) => VariableStack.Push(() => "5");
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
        dict[OpCodes.Ldloc_S] = (method, instruction) => VariableStack.Push(LocalsStack.GetLastElement(GetLocalVariableStackOffset(method, ((VariableDefinition)instruction.Operand).Index)));
        dict[OpCodes.Ldloca_S] = Impl_UnimplementedOp;
        dict[OpCodes.Stloc_S] = (method, instruction) => Impl_Stloc(method, ((VariableDefinition)instruction.Operand).Index);
        dict[OpCodes.Ldnull] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_M1] = Impl_UnimplementedOp;
        dict[OpCodes.Ldc_I4_0] = (method, instruction) => VariableStack.Push(() => "0");
        dict[OpCodes.Ldc_I4_1] = (method, instruction) => VariableStack.Push(() => "1");
        dict[OpCodes.Ldc_I4_2] = (method, Instruction) => VariableStack.Push(() => "2");
        dict[OpCodes.Stloc_3] = (method, instruction) => Impl_Stloc(method, 3);
        dict[OpCodes.Conv_U4] = Impl_UnimplementedOp;
        dict[OpCodes.Ble_Un_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, "<=");
        dict[OpCodes.Br] = (method, instruction) => Impl_Jump_If(method, instruction, 0, () => "True");
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

        dict[OpCodes.Brfalse] = (method, instruction) => dict[OpCodes.Brfalse_S](method, instruction);
        dict[OpCodes.Brtrue] = (method, instruction) => dict[OpCodes.Brtrue_S](method, instruction);

        dict[OpCodes.Beq] = Impl_UnimplementedOp;
        dict[OpCodes.Bge] = Impl_UnimplementedOp;
        dict[OpCodes.Bgt] = Impl_UnimplementedOp;
        dict[OpCodes.Ble] = Impl_UnimplementedOp;
        dict[OpCodes.Blt] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, "<");
        dict[OpCodes.Bne_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Bge_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Bgt_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Ble_Un] = Impl_UnimplementedOp;
        dict[OpCodes.Blt_Un_S] = (method, instruction) => Impl_Jump_If_Compare(method, instruction, "<");
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

    IEnumerable<LazyString> Impl_Jump_If_Compare(MethodInfo method, Instruction instruction, string comparison)
    {
        return Impl_Jump_If(method, instruction, (a, b) => () => $"Compare({a()}, {comparison}, {b()})");
    }

    IEnumerable<LazyString> Impl_Jump_If(MethodInfo method, Instruction instruction, int valuesToPop, LazyString condition)
    {
        yield return SetGlobal(Variables.Temporary, ArraySlice(
            GetGlobal(Variables.VariableStack),
            () => "0",
            () => valuesToPop.ToString()));

        foreach (var action in VariableStack.Pop(valuesToPop))
            yield return action;

        yield return SkipIf(Not(condition), () => (JumpOffsetStack.Push(() => "").Count() + 1).ToString());

        foreach (var action in JumpOffsetStack.Push(GetJumpId(method, (Instruction)instruction.Operand)))
            yield return action;

        yield return () => "Loop;";
    }

    IEnumerable<LazyString> Impl_Jump_If(MethodInfo method, Instruction instruction, Func<LazyString, LazyString, LazyString> binaryCondition)
    {
        return Impl_Jump_If(method, instruction, 2, binaryCondition(ArraySubscript(GetGlobal(Variables.Temporary), () => "1"), ArraySubscript(GetGlobal(Variables.Temporary), () => "0")));
    }

    int GetStaticFieldIndex(object operand)
    {
        var field = (FieldDefinition)operand;
        if (!m_staticFieldToIndex.TryGetValue(field, out int index))
            index = m_staticFieldToIndex[field] = m_staticFieldToIndex.Count;
        return index;
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

    static IEnumerable<LazyString> Impl_Stloc(MethodInfo method, int localVariableIndex)
    {
        return LocalsStack.SetLastElement(GetLocalVariableStackOffset(method, localVariableIndex), VariableStack.GetLastElement(0)).
            Concat(VariableStack.Pop(1));
    }

    static int GetLocalVariableStackOffset(MethodInfo method, int localVariableIndex)
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
        var paramsVal = ArraySlice(
            GetGlobal(Variables.VariableStack),
            () => "0",
            () => targetMethod.Parameters.Count.ToString());
        foreach (var action in ParameterStack.Push(paramsVal))
            yield return action;
        foreach (var action in VariableStack.Pop(targetMethod.Parameters.Count))
            yield return action;

        foreach (var action in JumpOffsetStack.Push(GetJumpId(method, instruction.Next)))
            yield return action;
        foreach (var action in JumpOffsetStack.Push(() => "0"))
            yield return action;

        foreach (var action in Impl_Call_CustomMethod_Direct(targetMethod))
            yield return action;

        yield return () => "Loop;";
    }

    IEnumerable<LazyString> Impl_Call_CustomMethod_Direct(LazyString functionId, int numLocals)
    {
        foreach (var action in CreateLocals(numLocals))
            yield return action;

        foreach (var action in CallStack.Push(functionId))
            yield return action;
    }

    IEnumerable<LazyString> Impl_Call_CustomMethod_Direct(MethodDefinition targetMethod)
    {
        var functionId = GetFunctionId(targetMethod);
        return Impl_Call_CustomMethod_Direct(() => functionId.ToString(), GetNumLocalVariables(targetMethod));
    }

    static IEnumerable<LazyString> CreateLocals(int numLocals)
    {
        // TODO: optimize this
        foreach (var i in Enumerable.Range(0, numLocals))
            foreach (var action in LocalsStack.Push(() => "0"))
                yield return action;
    }

    static IEnumerable<LazyString> Impl_Call_WorkshopAction(MethodInfo method, Instruction instruction, MethodReference targetMethodRef, string codeOverride = null)
    {
        // TODO: assert that the method reference is a workshop action

        // implicit conversions of Array types
        if (targetMethodRef.Name == "op_Implicit")
            yield break;

        var hasReturn = targetMethodRef.ReturnType.Name != "Void";
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

        if (hasReturn)
            yield return SetGlobal(Variables.Temporary, () => code);
        else
            yield return () => code + ";";

        if (DoesMethodNeedWait(code))
            yield return () => "Wait(0, Ignore Condition);";

        foreach (var action in VariableStack.Pop(targetMethodRef.Parameters.Count))
            yield return action;

        if (hasReturn)
            foreach (var action in VariableStack.Push(GetGlobal(Variables.Temporary)))
                yield return action;
    }

    static bool DoesMethodNeedWait(string code)
    {
        // temp variable fix
        var parenIndex = code.IndexOf('(');
        if (parenIndex == -1)
            return false;
        var codeName = code.Substring(0, parenIndex);
        var workshopCodeAttribute = s_workshopCodeAttributesByCodeName[codeName].FirstOrDefault();
        return workshopCodeAttribute?.NeedsWait ?? false;
    }

    static string GetCodeName(System.Reflection.MemberInfo targetMethod)
    {
        return GetWorkshopCodeAttribute(targetMethod)?.Name;
    }

    static WorkshopCodeAttribute GetWorkshopCodeAttribute(System.Reflection.MemberInfo targetMethod)
    {
        var attributes = targetMethod?.GetCustomAttributes(typeof(WorkshopCodeAttribute), true);
        var workshopCodeAttribute = (attributes?.FirstOrDefault() as WorkshopCodeAttribute);
        return workshopCodeAttribute;
    }

    static string GetCodeName(ISymbol targetMethod)
    {
        return targetMethod.GetAttributes().
            FirstOrDefault(attr => attr.AttributeClass.Name == typeof(WorkshopCodeAttribute).Name)?.
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

    IEnumerable<LazyString> Impl_SkipChainActions(MethodInfo method, Instruction instruction)
    {
        Func<int> thisLength = () => Impl_SkipChainActions(method, instruction).Count();
        yield return Skip(() => (thisLength() - 1).ToString());

        LazyString actionCountStr = () =>
        {
            var nextInChain = FindNextSkipChain(method, instruction);
            var actionCount = nextInChain == null ? 99999999 : CalcNumActionsToSkip(method, nextInChain) - CalcNumActionsToSkip(method, instruction);
            return actionCount.ToString();
        };

        yield return SetGlobal(Variables.Temporary2, GetGlobal(Variables.Temporary));
        yield return SetGlobal(Variables.Temporary, Subtract(GetGlobal(Variables.Temporary), actionCountStr));

        // skip to the actual action or skip to the next chain
        // add 1 to ignore the initial Skip, but subtract the length of this whole thing
        yield return Skip(Min(GetGlobal(Variables.Temporary2), Subtract(actionCountStr, () => (thisLength() - 1).ToString())));
    }

    Instruction FindNextSkipChain(MethodInfo method, Instruction instruction)
    {
        return method.Instructions.SkipWhile(i => i != instruction).Skip(1).FirstOrDefault(i => i.OpCode.Code == Code.No);
    }

    static IEnumerable<LazyString> Impl_Starg_S(MethodInfo method, Instruction instruction)
    {
        var parameters = method.Definition.Parameters;
        var paramCount = parameters.Count;

        yield return SetGlobal(Variables.Temporary, ArraySlice(
            GetGlobal(Variables.ParameterStack),
            () => "0",
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

    static IEnumerable<LazyString> DoBinaryOp(Func<LazyString, LazyString, LazyString> binaryOp)
    {
        // store the last two variables in temp
        yield return SetGlobal(Variables.Temporary, VariableStack.GetLastElement(1));
        yield return ArrayAppend(Variables.Temporary, VariableStack.GetLastElement(0));

        // pop them off the stack
        foreach (var action in VariableStack.Pop(2))
            yield return action;

        // push the addition of them onto the stack
        foreach (var action in VariableStack.Push(binaryOp(ArraySubscript(GetGlobal(Variables.Temporary), 1), ArraySubscript(GetGlobal(Variables.Temporary), 0))))
            yield return action;
    }

    int CalcNumActionsToSkip(MethodInfo method, Instruction target)
    {
        return method.Instructions.TakeWhile(i => i != target).Sum(i => ToWorkshopActions(method, i).Count());
    }

    Dictionary<MethodDefinition, int> m_functionIds;
    int GetFunctionId(MethodDefinition method)
    {
        return m_functionIds[method];
    }

    Dictionary<string, string> m_generatedMethodToWorkshopCode = new Dictionary<string, string>();
    int m_maxNumLocalVariables;

    static ILookup<string, WorkshopCodeAttribute> s_workshopCodeAttributesByCodeName;

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
        var generatedClassTree = substituter.GetGeneratedClass();

        compilation = compilation.RemoveAllSyntaxTrees();
        compilation = compilation.AddSyntaxTrees(newTree, generatedClassTree);

        Console.WriteLine(generatedClassTree.ToString());

        var result = compilation.Emit("AsmBuild.dll");
        if (!result.Success)
        {
            foreach (var diag in result.Diagnostics)
                Console.WriteLine(diag);
            throw new System.Exception("Fail!");
        }

        var module = ModuleDefinition.ReadModule("AsmBuild.dll");
        var mainClass = module.GetType("MainClass");
        var methods = mainClass.Methods;
        GenerateFunctionIds(methods);
        m_maxNumLocalVariables = methods.Max(m => m.Body.Variables.Count);

        var ruleWriter = new StringWriter();

        var staticConstructor = methods.SingleOrDefault(m => m.Name == ".cctor");
        var mainMethod = methods.Single(m => m.Name == "Main");
        var entryPointActionsText = EntryPointActions(staticConstructor, mainMethod).Select(a => $"        {a()}").ListToString("\n");
        ruleWriter.WriteLine(GenerateRule("EntryPoint", "Ongoing - Global;", "", entryPointActionsText));

        var updateMethod = methods.SingleOrDefault(m => m.Name == "Update");
        var taskRunnerActionsText = TaskRunnerActions(updateMethod).Select(a => $"        {a()}").ListToString("\n");
        ruleWriter.WriteLine(GenerateRule("TaskRunner", "Ongoing - Global;", $"{FunctionCondition(0)()} == True;", taskRunnerActionsText));

        foreach (var method in methods)
        {
            var methodInfo = new MethodInfo
            {
                Definition = method,
                Instructions = method.Body.Instructions.ToList(),
            };

            InsertSkipChainInstructions(methodInfo);

            // TODO: add analyzer to prevent storing un-storable variables on stack (Array, Player, etc)

            Console.WriteLine(method);
            foreach (var instr in methodInfo.Instructions)
                Console.WriteLine(instr);
            Console.WriteLine();

            ConvertMethodToRule(ruleWriter, methodInfo);
        }

        return ruleWriter.ToString();
    }

    // the Skip action maxes out at 99, WTF.
    // to get around this, make sure there's always a place to skip to
    // in order to get to the end of the method
    void InsertSkipChainInstructions(MethodInfo methodInfo)
    {
        var lastSkipChainActionIndex = 0;
        for (var i = 0; i != methodInfo.Instructions.Count; ++i)
        {
            var actionIndex = CalcNumActionsToSkip(methodInfo, methodInfo.Instructions[i]);
            if (actionIndex - lastSkipChainActionIndex > 75)
            {
                var instruction = Instruction.Create(OpCodes.Ret);
                instruction.OpCode = OpCodes.No;
                instruction.Previous = methodInfo.Instructions[i - 1];
                instruction.Next = methodInfo.Instructions[i];
                methodInfo.Instructions.Insert(i, instruction);
                lastSkipChainActionIndex = actionIndex;
            }
        }
    }

    IEnumerable<LazyString> EntryPointActions(MethodDefinition staticConstructor, MethodDefinition mainMethod)
    {
        yield return SetGlobal(Variables.TaskQueue, EmptyArray());
        yield return SetGlobal(JumpOffsetStack.stackVar, CreateArray(1));
        yield return SetGlobal(LocalsStack.stackVar, CreateArray(1));
        yield return SetGlobal(ParameterStack.stackVar, CreateArray(1));
        yield return SetGlobal(VariableStack.stackVar, CreateArray(1));

        yield return SetGlobal(Variables.StaticFields, CreateArray(m_staticFieldToIndex.Count));

        foreach (var action in Impl_Call_CustomMethod_Direct(mainMethod))
            yield return action;

        if (staticConstructor != null)
        {
            // call static constructor "after" main, since call just pushes it on the stack and
            // it'll actually call in reverse order
            foreach (var action in Impl_Call_CustomMethod_Direct(staticConstructor))
                yield return action;
        }
    }

    IEnumerable<LazyString> TaskRunnerActions(MethodDefinition updateMethod)
    {
        yield return () => $"Abort If (Not({FunctionCondition(0)()}));";
        yield return () => "Wait(0, Ignore Condition);";

        foreach (var action in TaskQueue.PopTaskTo(Variables.Temporary))
            yield return action;

        var functionId = ArraySubscript(GetGlobal(Variables.Temporary), 0);

        // if there's no function, call Update (if it exists, otherwise just loop)
        var updateCallActions = updateMethod == null ? new LazyString[0] : Impl_Call_CustomMethod_Direct(updateMethod);
        yield return SkipIf(NotEqual(functionId, () => "0"), () => (updateCallActions.Count() + 1).ToString());
        foreach (var action in updateCallActions)
            yield return action;
        yield return () => "Loop;";

        // push event params onto stack before calling
        foreach (var action in ParameterStack.Push(ArraySlice(GetGlobal(Variables.Temporary), () => "1", () => "3")))
            yield return action;

        foreach (var action in Impl_Call_CustomMethod_Direct(functionId, m_maxNumLocalVariables))
            yield return action;

        yield return () => "Loop;";
    }

    void GenerateFunctionIds(IEnumerable<MethodDefinition> methods)
    {
        // start at 1 so that 0 doesn't set any functions off
        m_functionIds = methods.Zip(Enumerable.Range(1, methods.Count()), (m, i) => (m, i)).ToDictionary(pair => pair.m, pair => pair.i);
    }

    static void Main(string[] args)
    {
        var transpiler = new Transpiler();
        var source = File.ReadAllText(args.FirstOrDefault() ?? "Test\\Test.cs");
        var rules = transpiler.TranspileToRules(source);
        Console.WriteLine(rules);
        TextCopy.Clipboard.SetText(rules);

        Console.WriteLine("Copied rules to clipboard. Open the workshop and click paste.");
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
        {0}
    }}";

    static string ConditionsFormat => @"
    conditions
    {{
        {0}
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

        var actionCount = 0;
        writeLine("// Body");
        foreach (var instr in method.Instructions)
        {
            var numActions = ToWorkshopActions(method, instr).Count();
            writeLine($"    // [{actionCount}] {instr}");
            actionCount += numActions;
            foreach (var line in ToWorkshopActions(method, instr))
                writeLine(line());
        }

        var workshopEventAttr = GetCustomAttribute<WorkshopEventAttribute>(method);
        if (workshopEventAttr != null)
        {
            Debug.Assert(method.Definition.Parameters.Count == 3, $"Event function {method.Definition.Name} must have the following parameter list: (Player eventPlayer, float eventDamage, bool eventWasCriticalHit)");
            ruleWriter.WriteLine(GenerateRule(
                $"Event Task for: {method.Definition.Name}",
                GetCodeName(typeof(Workshop.Event).GetMember(workshopEventAttr.m_event.ToString()).First()),
                $"",
                PushTaskForEventMethod(method)()));
        }

        ruleWriter.WriteLine(GenerateRule(
            method.Definition.Name,
            "Ongoing - Global;",
            $"{FunctionCondition(method)()} == True;",
            writer.ToString()));
    }

    LazyString PushTaskForEventMethod(MethodInfo method)
    {
        LazyString functionId = () => GetFunctionId(method.Definition).ToString();
        var taskValues = CreateArray(functionId, () => "EVENT PLAYER", () => "EVENT DAMAGE", () => "EVENT WAS CRITICAL HIT");
        return TaskQueue.PushTask(taskValues);
    }

    static AttributeType GetCustomAttribute<AttributeType>(MethodInfo method) where AttributeType : class
    {
        var customAttr = GetCustomAttributeData<AttributeType>(method);
        if (customAttr == null)
            return null;

        var args = customAttr.ConstructorArguments.Select(arg => arg.Value).ToArray();
        if (typeof(AttributeType) == typeof(WorkshopEventAttribute))
            args[0] = (Workshop.Event)args[0];
        return Activator.CreateInstance(typeof(AttributeType), args) as AttributeType;
    }

    static CustomAttribute GetCustomAttributeData<AttributeType>(MethodInfo method) where AttributeType : class
    {
        return method.Definition.CustomAttributes.FirstOrDefault(attr => attr.AttributeType.Name == typeof(AttributeType).Name);
    }

    static string GenerateRule(string name, string eventText, string conditionsText, string actionsText)
    {
        return string.Format(
            RuleFormat,
            name,
            string.Format(EventFormat, eventText),
            string.Format(ConditionsFormat, conditionsText),
            string.Format(ActionsFormat, actionsText));
    }

    LazyString FunctionCondition(MethodInfo method)
    {
        var functionId = GetFunctionId(method.Definition);
        return FunctionCondition(functionId);
    }

    static LazyString FunctionCondition(int functionId)
    {
        return Equal(ArrayFirst(GetGlobal(CallStack.stackVar)), () => functionId.ToString());
    }
}
