using System;
using Workshop;
using static Workshop.Values;
using static Workshop.Actions;

public static class MainClass
{
    // [WorkshopEvent(Event.PlayerDealtDamage)]
    // public static void PlayerDealtDamage(Player eventPlayer, float eventDamage, bool eventWasCriticalHit)
    // {
    //     BigMessage(eventPlayer, String("{0} {1}", eventDamage, String("Damage")));
    // }

    // public static float Fibonacci(int n, float a = 0, float b = 1)
    // {
    //     return n == 1 ? b : Fibonacci(n - 1, b, a + b);
    // }

    // public static float Fibonacci(int n)
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

    static Vector CenterOfLighthouse => Vector(290.22f, -25.47f, -88.76f);

    static int s_count = 0;
    public static void Update()
    {
        SetPlayerAllowedHeroes(AllPlayers(Team.All), Hero.WreckingBall);

        for (var i = 0; i < NumberOfPlayers(Team.All); ++i)
        {
            var player = AllPlayers(Team.All).ValueInArray(i);
            Teleport(player, CenterOfLighthouse);
            var position = PositionOf(player);
            BigMessage(player, String("({0})", position));
        }
    }

    public static void Main()
    {
    }
}
