// -----------------------------------------------------------------------
// <copyright file="CursedPlayerEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Player;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Handlers;

public static class CursedPlayerEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerConnectedEventArgs> Connected;
    
    public static event EventManager.CursedEventHandler<PlayerDisconnectingEventArgs> Disconnecting;
    
    public static event EventManager.CursedEventHandler<PlayerDisconnectedEventArgs> Disconnected;
    
    public static event EventManager.CursedEventHandler<PlayerChangingRoleEventArgs> ChangingRole;
    
    public static event EventManager.CursedEventHandler<PlayerSpawningEventArgs> Spawning;
    
    public static event EventManager.CursedEventHandler<PlayerReceivingDamageEventArgs> ReceivingDamage;
    
    public static event EventManager.CursedEventHandler<PlayerDyingEventArgs> Dying;
    
    public static event EventManager.CursedEventHandler<PlayerEscapingEventArgs> Escaping;

    public static event EventManager.CursedEventHandler<PlayerUsingVoiceChatEventArgs> UsingVoiceChat;
    
    public static event EventManager.CursedEventHandler<PlayerTogglingNoClipEventArgs> TogglingNoClip;

    internal static void OnPlayerConnected(PlayerConnectedEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Connected.InvokeEvent(args);
    }

    internal static void OnPlayerDisconnecting(PlayerDisconnectingEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Disconnecting.InvokeEvent(args);
    }
    
    internal static void OnPlayerDisconnected(PlayerDisconnectedEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Disconnected.InvokeEvent(args); 
        CursedLogger.InternalDebug("Removing player");
        CursedPlayer.Dictionary.Remove(args.Player.ReferenceHub);
    }

    internal static void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        ChangingRole.InvokeEvent(args);
    }

    internal static void OnPlayerSpawning(PlayerSpawningEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Spawning.InvokeEvent(args);
    }

    internal static void OnPlayerReceivingDamage(PlayerReceivingDamageEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        ReceivingDamage.InvokeEvent(args);
    }
    
    internal static void OnPlayerDying(PlayerDyingEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Dying.InvokeEvent(args);
    }
    
    internal static void OnPlayerEscaping(PlayerEscapingEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Escaping.InvokeEvent(args);
    }
    
    internal static void OnPlayerUsingVoiceChat(PlayerUsingVoiceChatEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        UsingVoiceChat.InvokeEvent(args);
    }

    internal static void OnPlayerTogglingNoClip(PlayerTogglingNoClipEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        TogglingNoClip.InvokeEvent(args);
    }
}