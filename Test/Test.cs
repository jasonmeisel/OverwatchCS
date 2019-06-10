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
            // Workshop.Actions.DebugLog(RecursiveFibonacci(10));
            Workshop.Actions.DebugLog(RecursiveFibonacci(++i, 0, 1));
            // Workshop.Actions.DebugLog(++i);
            Workshop.Actions.Wait(1);
        }
    }

    public static void TestLoopArray()
    {
        var players = Workshop.Values.AllPlayers(Workshop.Team.All());
        var value = Workshop.Values.LastOf(players);
        // for (int i = 0; i != 1337; ++i)
        //     Workshop.Actions.Wait(0.01f);
    }

    public static float RecursiveFibonacci(int n, float a = 0, float b = 1)
    {
    	if (n == 1)
            return b;
        return RecursiveFibonacci(n - 1, b, a + b);
    }

    public static void Main()
    {
        TestWait();
    }
}
