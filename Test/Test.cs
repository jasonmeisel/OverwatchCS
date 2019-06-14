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

    static readonly Vector PlayCenter = Vector(290.22f, -25.47f, -88.76f);
    static readonly float PlayRadius = 10;

    public static void Update()
    {
        // SetPlayerAllowedHeroes(AllPlayers(Team.All), Hero.WreckingBall);
        
        if (IsBetweenRounds() || IsInSetup())
            return;

        for (var playerIndex = 0; playerIndex < NumberOfPlayers(Team.All); ++playerIndex)
        {
            var player = AllPlayers(Team.All).ValueInArray(playerIndex);

            if (HasSpawned(player) && !IsInSpawnRoom(player))
            {
                // when first spawned
                if (!GetPlayerVariable<bool>(player, 'A'))
                {
                    OnPlayerSpawned(playerIndex, player);
                }

                var playerPosition = PositionOf(player);
                var distToCenter = DistanceBetween(playerPosition, PlayCenter);
                if (distToCenter > PlayRadius)
                {
                    Damage(player, null, 1);

                    StartAccelerating(player, PlayCenter - playerPosition, 1000, 5, RelativeTo.World);

                    var explosionPosition = Normalize(playerPosition - PlayCenter) * PlayRadius + PlayCenter;
                    PlayEffect(AllPlayers(Team.All), PlayEffectType.BadExplosion, Color.Red, explosionPosition, 1);
                    PlayEffect(AllPlayers(Team.All), PlayEffectType.RingExplosionSound, Color.Red, explosionPosition, 1);
                }
                else
                {
                    StopAccelerating(player);
                }
                BigMessage(player, String("({0})", playerPosition));
            }
        }
    }

    static void OnPlayerSpawned(int playerIndex, Player player)
    {
        SetPlayerVariable(player, 'A', true);

        var offset = (PlayRadius * 0.9f) * CircleVectorAtAngle(playerIndex * 360);
        Teleport(player, PlayCenter + offset);

        for (var angle = 0.0f; angle < 360.0f; angle += 5)
        {
            CreateEffect(
                player, CreateEffectType.Cloud, Color.Red,
                PlayCenter + PlayRadius * CircleVectorAtAngle(angle),
                0.5f);
        }
    }

    static Vector CircleVectorAtAngle(float angle)
    {
        return Vector(SineFromDegrees(angle), 0, CosineFromDegrees(angle));
    }

    // public static void Update()
    // {
    //     SetPlayerAllowedHeroes(AllPlayers(Team.All), Hero.WreckingBall);

    //     for (var i = 0; i < NumberOfPlayers(Team.All); ++i)
    //     {
    //         var player = AllPlayers(Team.All).ValueInArray(i);
    //         var position = PositionOf(player);
    //         BigMessage(player, String("({0})", position));
    //     }
    // }

    // public static void LongTest()
    // {
    //     if (s_count == 0)
    //     {
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //     }
    //     if (s_count == 0)
    //     {
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //     }
    //     if (s_count == 0)
    //     {
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //     }
    //     if (s_count == 0)
    //     {
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //     }
    //     if (s_count == 0)
    //     {
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //         Actions.StartCamera();
    //     }
    // }

    public static void Main()
    {
    }
}
