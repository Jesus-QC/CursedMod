---
description: >-
  More Effective Coroutines is an improved implementation of coroutines that
  runs about twice as fast as Unity’s coroutines do and has zero per-frame
  memory allocations.
---

# ⌛ MEC

You should use MEC for small things like delaying actions for some seconds or creating loops.

## Examples

### Simple Delayed Action

To delay actions with MEC you just have to call `Timing.CallDelayed(float, Action)` which executes the action after a given number of seconds passes.

```csharp
using MEC;

public void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
{
    Timing.CallDelayed(3f, () => 
    {
        args.Player.Position = Vector3.zero;
    });
}
```

### Simple Coroutine

In certain actions you may want to delay some step for a frame or a couple seconds. Coroutines are handy in this type of cases. To create a coroutine you may follow the next example:

```csharp
using MEC;

public IEnumerator<float> MyCoroutine(Player player) // You can pass arguments
{
    player.Role = RandomRoles.GetOne();
    yield return Timing.WaitForSeconds(5); // Stops execution for 5 seconds
    player.ShowBroadcast("See you in the next frame!");
    yield return Timing.WaitForOneFrame; // Stops execution for 1 frame
    player.ShowBroadcast("There you are!")
}
```

However you can't run the method directly, you need to use a special method to run the coroutine:

```csharp
public void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
{
    Timing.RunCoroutine(MyCoroutine(args.Player));
}
```

### Simple Loop

With the already known concepts we can do a simple loop to repeat things over time.

```csharp
public IEnumerator<float> MyLoopCoroutine()
{
    while (true) // or for(;;)
    {
        CursedFacility.ShowBroadcast("See you all in 5 minutes!");
        yield return Timing.WaitForSeconds(600); // wait for 5 minutes
    }
}
```
