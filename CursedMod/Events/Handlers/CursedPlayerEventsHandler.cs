﻿// -----------------------------------------------------------------------
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
    public static event CursedEventManager.CursedEventHandler<PlayerConnectedEventArgs> Connected;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDisconnectingEventArgs> Disconnecting;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDisconnectedEventArgs> Disconnected;
    
    public static event CursedEventManager.CursedEventHandler<PlayerChangingRoleEventArgs> ChangingRole;
    
    public static event CursedEventManager.CursedEventHandler<PlayerSpawningEventArgs> Spawning;
    
    public static event CursedEventManager.CursedEventHandler<PlayerReceivingDamageEventArgs> ReceivingDamage;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDyingEventArgs> Dying;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDiedEventArgs> Died;
    
    public static event CursedEventManager.CursedEventHandler<PlayerEscapingEventArgs> Escaping;

    public static event CursedEventManager.CursedEventHandler<PlayerUsingVoiceChatEventArgs> UsingVoiceChat;
    
    public static event CursedEventManager.CursedEventHandler<PlayerTogglingNoClipEventArgs> TogglingNoClip;

    public static event CursedEventManager.CursedEventHandler<PlayerPlacingBulletHoleEventArgs> PlacingBulletHole;
    
    public static event CursedEventManager.CursedEventHandler<PlayerPlacingBloodDecalEventArgs> PlacingBloodDecal;

    internal static void OnPlayerConnected(PlayerConnectedEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Connected.InvokeEvent(args);
        CursedDesyncModule.HandlePlayerConnected(args);
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
    
    internal static void OnPlayerDied(PlayerDiedEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        Died.InvokeEvent(args);
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

    internal static void OnPlayerPlacingBulletHole(PlayerPlacingBulletHoleEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlacingBulletHole.InvokeEvent(args);
    }
    
    internal static void OnPlayerPlacingBloodDecal(PlayerPlacingBloodDecalEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlacingBloodDecal.InvokeEvent(args);
    }
}
