using System;
using CursedMod.Events.Arguments.Player;
using CursedMod.Features.Wrappers.Player;
using PluginAPI.Core;

namespace CursedMod.Events.Handlers.Player;

public static class PlayerEventHandlers
{
    public static event EventManager.CursedEventHandler<PlayerJoinedEventArgs> Joined;

    public static void OnPlayerJoined(PlayerJoinedEventArgs args)
    {
        Joined.InvokeEvent(args);
    }
    
    public static event EventManager.CursedEventHandler<PlayerDisconnectedEventArgs> Disconnected;

    public static void OnPlayerDisconnected(PlayerDisconnectedEventArgs args)
    {
        Disconnected.InvokeEvent(args);
        CursedPlayer.Dictionary.Remove(args.Player.ReferenceHub);
    }
    
    public static event EventManager.CursedEventHandler<PlayerChangingRoleEventArgs> ChangingRole;

    public static void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
    {
        ChangingRole.InvokeEvent(args);
    }
}