﻿using System;
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

    public static float Fibonacci(int n, float a = 0, float b = 1)
    {
        return n == 1 ? b : Fibonacci(n - 1, b, a + b);
    }

    static int s_count = 0;
    public static void Update()
    {
        BigMessage(AllPlayers(Team.All), String("({0})", Fibonacci(s_count++ % 20)));
        // SetPlayerAllowedHeroes(AllPlayers(Team.All), Hero.WreckingBall);

        // for (var i = 0; i < NumberOfPlayers(Team.All); ++i)
        // {
        //     var player = AllPlayers(Team.All).ValueInArray(i);
        //     var position = PositionOf(player);
        //     BigMessage(player, String("({0})", position));
        // }
    }

    public static void Main()
    {
    }
}
