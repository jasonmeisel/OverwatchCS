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

    public static void Update()
    {
        SetPlayerAllowedHeroes(AllPlayers(Team.All), Hero.WreckingBall);

        for (var i = 0; i < NumberOfPlayers(Team.All); ++i)
        {
            var player = AllPlayers(Team.All).ValueInArray(i);
            var position = PositionOf(player);
            BigMessage(player, String("({0})", position));
        }
    }

    public static void Main()
    {
    }
}
