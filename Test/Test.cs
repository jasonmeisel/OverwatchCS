using System;

public static class MainClass
{
    public static float FibonacciNumber(int n)
    {
        var a = 0.0f;
        var b = 1.0f;
        while (--n != 0)
        {
            var c = a + b;
            a = b;
            b = c;
        }
        return b;
    }

    public static void TestWait()
    {
        var i = 0;
        while (true)
        {
            Workshop.Actions.DebugLog(i++);
            Workshop.Actions.Wait(1);
        }
    }

    // public static float RecursiveFibonacci(int n, float a = 0, float b = 1)
    // {
    // 	if (n == 1)
    //         return b;
    //     return RecursiveFibonacci(n - 1, b, a + b);
    // }
}
