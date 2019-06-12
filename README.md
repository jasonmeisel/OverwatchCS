Transpiler for C# to Overwatch Workshop code!

## Features:
- [x] Basic C# support (local variables, conditionals, loops, arithmetic)
```cs
var a = 0.0f;
var b = 1.0f;
while (--n != 0)
{
    var c = a + b;
    a = b;
    b = c;
}
return b;
```

- [x] Workshop actions and values mapped to functions
```cs
var player = AllPlayers(Team.All()).ValueInArray(0);
BigMessage(AllPlayers(Team.All()), String("{0} {1}", player, HeroOf(player)));
```

- [x] Static method calls (call stack)
```cs
public static float Fibonacci(int n, float a = 0, float b = 1)
{
    return n == 1 ? b : Fibonacci(n - 1, b, a + b);
}
```

- [x] Static fields (global variables)
```cs
static int s_count = 0;

public static void ShowCount()
{
    BigMessage(AllPlayers(Team.All), String("({0})", s_count++));
}
```

- [x] Workshop Events
```cs
[WorkshopEvent(Event.PlayerDealtDamage)]
public static void PlayerDealtDamage(Player eventPlayer, float eventDamage, bool eventWasCriticalHit)
{
    BigMessage(eventPlayer, String("{0} {1}", eventDamage, String("Damage")));
}
```
