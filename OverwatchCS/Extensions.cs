using System.Collections.Generic;

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
