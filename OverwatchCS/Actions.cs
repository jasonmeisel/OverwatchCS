using System.Linq;
using System.Collections.Generic;

using LazyString = System.Func<string>;
using Variables = Workshop.Globals;

public static class Actions
{
    public class Stack
    {
        public char stackVar;

        public IEnumerable<LazyString> Push(LazyString value)
        {
            // push to front
            yield return SetGlobal(stackVar, ArrayConcat(CreateArray(value), GetGlobal(stackVar)));
        }

        public IEnumerable<LazyString> Pop(int count)
        {
            // pop from front
            yield return SetGlobal(stackVar, ArraySlice(GetGlobal(stackVar), () => count.ToString(), () => "99999999"));
        }

        public LazyString GetLastElement(int offset)
        {
            return ArraySubscript(GetGlobal(stackVar), () => offset.ToString());
        }

        public IEnumerable<LazyString> SetLastElement(int offset, LazyString value)
        {
            yield return SetGlobalAtIndex(stackVar, () => offset.ToString(), value);
        }
    }

    public static class TaskQueue
    {
        static char QueueVar => Variables.TaskQueue;

        public static LazyString PushTask(LazyString value)
        {
            // push to the end, that way we can pop without knowing the size
            return SetGlobal(QueueVar, ArrayConcat(GetGlobal(QueueVar), value));
        }

        public static IEnumerable<LazyString> PopTaskTo(char variable)
        {
            yield return SetGlobal(variable, ArraySlice(GetGlobal(QueueVar), () => "0", () => "4"));
            yield return SetGlobal(QueueVar, ArraySlice(GetGlobal(QueueVar), () => "4", () => "9999999"));
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

    public static LazyString SetGlobalAtIndex(char variable, LazyString index, LazyString value)
    {
        return () => $"Set Global Variable At Index({variable}, {index()}, {value()});";
    }

    public static LazyString SetGlobalAtIndex(char variable, int index, LazyString value)
    {
        return SetGlobalAtIndex(variable, () => index.ToString(), value);
    }

    public static LazyString ArrayIndexOf(LazyString array, LazyString value)
    {
        return () => $"Index Of Array Value({array()}, {value()})";
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

    public static LazyString ArrayFirst(LazyString array)
    {
        return () => $"First Of({array()})";
    }

    public static LazyString ArrayLast(LazyString array)
    {
        return () => $"Last Of({array()})";
    }

    public static LazyString ArrayConcat(LazyString a, LazyString b)
    {
        return () => $"Append To Array({a()}, {b()})";
    }

    public static LazyString CreateArray(params LazyString[] values)
    {
        if (values.Length == 0)
            return EmptyArray();
        return ArrayConcat(CreateArray(values.Take(values.Length - 1).ToArray()), values.Last());
    }

    public static LazyString ResizeArray(char stackVar, LazyString size)
    {
        return SetGlobal(stackVar, ArraySlice(GetGlobal(stackVar), () => "0", size));
    }

    public static LazyString EmptyArray()
    {
        return () => "Empty Array";
    }

    public static LazyString CreateArray(int count)
    {
        if (count == 0)
            return EmptyArray();
        return ArrayConcat(CreateArray(count - 1), () => "0");
    }

    public static Stack VariableStack = new Stack { stackVar = Variables.VariableStack };
    public static Stack ParameterStack = new Stack { stackVar = Variables.ParameterStack };
    public static Stack CallStack = new Stack { stackVar = Variables.CallStack };
    public static Stack JumpOffsetStack = new Stack { stackVar = Variables.JumpOffsetStack };
    public static Stack LocalsStack = new Stack { stackVar = Variables.LocalsStack };

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

    public static LazyString Min(LazyString valueA, LazyString valueB)
    {
        return () => $"Min({valueA()}, {valueB()})";
    }

    public static LazyString Max(LazyString valueA, LazyString valueB)
    {
        return () => $"Max({valueA()}, {valueB()})";
    }

    public static LazyString NotEqual(LazyString a, LazyString b)
    {
        return () => $"Compare({a()}, !=, {b()})";
    }

    public static LazyString Not(LazyString value)
    {
        return () => $"Not({value()})";
    }

    public static LazyString Equal(LazyString a, LazyString b)
    {
        return () => $"Compare({a()}, ==, {b()})";
    }

    public static LazyString Skip(LazyString actionCount)
    {
        // Skip(0) still skips one, because fml.
        // do a SkipIf instead to actually Skip(0)
        return () => $"Skip If({NotEqual(actionCount, () => "0")()}, {actionCount()});";
    }

    public static LazyString SkipIf(LazyString value, LazyString actionCount)
    {
        return () => $"Skip If({value()}, {actionCount()});";
    }

    public static LazyString LoopIf(LazyString value)
    {
        return () => $"Loop If({value()});";
    }
}
