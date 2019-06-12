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
    [WorkshopEvent(Event.PlayerDealtDamage)]
    public static void PlayerDealtDamage(Player eventPlayer, float eventDamage, bool eventWasCriticalHit)
    {
        BigMessage(eventPlayer, String("{0} {1} {2}", eventDamage, String("Damage"), String("Well Played")));
        // BigMessage(AllPlayers(Team.All), String("HELLO"));
    }

    static float s_lastHelloTime = -1;
    static int s_count = 0;

    public static void Update()
    {
        if (Values.TotalTimeElapsed() - s_lastHelloTime >= 1)
        {
            BigMessage(AllPlayers(Team.All), String("({0})", s_count++));
            s_lastHelloTime += 1;
        }
    }

    public static void Main()
    {
        // for (int count = 0; true; ++count)
        // {
        //     var numPlayers = NumberOfPlayers(Team.All);
        //     var index = count % numPlayers;
        //     var player = AllPlayers(Team.All).GetElement(index);
        //     BigMessage(AllPlayers(Team.All), String("{0} - {1}", index, player));
        //     Wait(1);
        // }
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
