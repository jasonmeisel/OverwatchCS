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
        SetPlayerAllowedHeroes(AllPlayers(Team.All), Hero.WreckingBall);

        if (IsBetweenRounds() || IsInSetup())
            return;

        for (var playerIndex = 0; playerIndex < NumberOfPlayers(Team.All); ++playerIndex)
        {
            var player = AllPlayers(Team.All).ValueInArray(playerIndex);

            if (IsDead(player))
            {
                SetPlayerVariable(player, 'A', false);
                SetMoveSpeed(player, 0);
            }

            if (TotalTimeElapsed() - GetPlayerVariable<float>(player, 'B') > 0.2f)
                StopAccelerating(player);

            if (HasSpawned(player) && !IsInSpawnRoom(player) && IsAlive(player))
            {
                if (!GetPlayerVariable<bool>(player, 'A'))
                {
                    OnPlayerSpawned(playerIndex, player);
                }

                var playerPosition = PositionOf(player);
                var playerFloorPosition = Vector(playerPosition.X(), PlayCenter.Y(), playerPosition.Z());

                BumpOtherPlayers(player);

                var distToCenter = DistanceBetween(playerFloorPosition, PlayCenter);
                if (distToCenter > PlayRadius)
                {
                    Damage(player, null, 25);

                    StartAccelerating(player, PlayCenter - playerFloorPosition, 1000, 100, RelativeTo.World, ReevaluationValue.None);

                    var explosionPosition = Normalize(playerFloorPosition - PlayCenter) * PlayRadius + PlayCenter;
                    PlayEffect(AllPlayers(Team.All), PlayEffectType.BadExplosion, Color.Red, explosionPosition, 1);
                    PlayEffect(AllPlayers(Team.All), PlayEffectType.RingExplosionSound, Color.Red, explosionPosition, 1);
                }
                else
                {
                    StopAccelerating(player);
                }

                BigMessage(player, String("({0})", playerFloorPosition));
            }
        }
    }

    static void BumpOtherPlayers(Player player)
    {
        for (var i = 0; i < NumberOfLivingPlayers(Team.All); ++i)
        {
            var otherPlayer = AllLivingPlayers(Team.All).GetElement(i);
            if (otherPlayer == player)
                continue;

            if (DistanceBetween(PositionOf(player), PositionOf(otherPlayer)) < 1)
            {
                StopAccelerating(otherPlayer);
                StartAccelerating(otherPlayer, VelocityOf(player), 10000, 100, RelativeTo.World, ReevaluationValue.None);
                SetPlayerVariable(otherPlayer, 'B', TotalTimeElapsed());
            }
        }
    }

    static void OnPlayerSpawned(int playerIndex, Player player)
    {
        SetPlayerVariable(player, 'A', true);
        SetMoveSpeed(player, 100);

        var offset = (PlayRadius * 0.9f) * CircleVectorAtAngle(playerIndex * 360);
        Teleport(player, PlayCenter + offset);
        SetFacing(player, Vector(0,0,0) - offset, RelativeTo.World);
    }

    static Vector CircleVectorAtAngle(float angle)
    {
        return Vector(SineFromDegrees(angle), 0, CosineFromDegrees(angle));
    }

    public static void Main()
    {
        // create ring of fire
        for (var angle = 0.0f; angle < 360.0f; angle += 5)
        {
            CreateEffect(
                AllPlayers(Team.All), CreateEffectType.Cloud, Color.Red,
                PlayCenter + PlayRadius * CircleVectorAtAngle(angle),
                0.5f, ReevaluationValue.VisibleTo);
        }
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
}
