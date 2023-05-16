---
description: >-
  The main function of the API is to provide a rich variation of events you may
  subscribe to.
---

# ⚡ Using events

## Subscribing to events

All your event subscriptions should be done inside your module entry point:

```csharp
public override void OnLoaded()
{
    CursedPlayerEventsHandler.Joined += OnPlayerJoined;
    
    base.OnLoaded(); 
}
```

## Desubscribing to events

All your event desubscriptions should be done inside your module exit point:

```csharp
public override void OnUnloaded()
{
    CursedPlayerEventsHandler.Joined -= OnPlayerJoined;
    
    base.OnUnloaded(); 
}
```

## Using events

Each event has a different usage. Whenever you subscribe a method to an event it should have an event args parameter following the next example:

```csharp
public void OnPlayerJoined(PlayerJoinedEventArgs args)
{
    // Your code 
}
```

Some events don't need any type of argument since they are static temporary events such as Waiting For Players or Restarting Round.

## Using the arguments

Each argument has different properties you may use for your advantage

Some event arguments have a special property: `IsAllowed` This property allows events to be cancelled:

```csharp
public void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
{
    args.IsAllowed = false;
    // The player role won't change!
}
```

Cancellable events usually end with `-ing`

You can feel free to safely use any argument without issues:

```csharp
public void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
{
    args.Player.Username = "CursedMod is cool";
    args.NewRole = RoleTypeId.Tutorial;
}
```

Wow! You learn fast. That is all events provide. Now you can use them at your discretion.
