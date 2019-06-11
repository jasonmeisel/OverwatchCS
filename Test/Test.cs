using System;
using Workshop;
using static Workshop.Values;
using static Workshop.Actions;

public static class MainClass
{
    // public static float FibonacciNumber(int n)
    // {
    //     var a = 0.0f;
    //     var b = 1.0f;
    //     while (--n != 0)
    //     {
    //         var c = a + b;
    //         a = b;
    //         b = c;
    //     }
    //     return b;
    // }

    // public static void TestWait()
    // {
    //     var i = 0;
    //     while (true)
    //     {
    //         // Workshop.Actions.DebugLog(RecursiveFibonacci(10));
    //         Workshop.Actions.DebugLog(RecursiveFibonacci(++i, 0, 1));
    //         // Workshop.Actions.DebugLog(++i);
    //         Workshop.Actions.Wait(1);
    //     }
    // }

    // public static void Main()
    // {
    //     for (int i = 0; true; ++i)
    //     {
    //         var numPlayers = NumberOfPlayers(Team.All());
    //         var player = AllPlayers(Team.All()).ValueInArray(i % numPlayers);
    //         BigMessage(AllPlayers(Team.All()), String("{0} {1}", player, HeroOf(player)));
    //         Wait(1);
    //     }
    // }

    static void PrintPlayerAndHero(Player player, Hero hero)
    {
        BigMessage(AllPlayers(Team.All), String("{0} -> {1}", player, hero));
    }

    public static void Main()
    {
        for (int index = 0; true; ++index)
        {
            var numPlayers = NumberOfPlayers(Team.All);
            var player = AllPlayers(Team.All).GetElement(index % numPlayers);
            PrintPlayerAndHero(player, HeroOf(player));
            Wait(1);
        }
    }

    // public static float RecursiveFibonacci(int n, float a = 0, float b = 1)
    // {
    // 	if (n == 1)
    //         return b;
    //     return RecursiveFibonacci(n - 1, b, a + b);
    // }

    // public static void Main()
    // {
    //     TestWait();
    // }
}
