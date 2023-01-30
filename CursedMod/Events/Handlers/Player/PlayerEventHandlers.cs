// -----------------------------------------------------------------------
// <copyright file="PlayerEventHandlers.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Events.Arguments.Player;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Player;
using PluginAPI.Core;

namespace CursedMod.Events.Handlers.Player;

public static class PlayerEventHandlers
{
    public static event EventManager.CursedEventHandler<PlayerJoinedEventArgs> Joined;
    
    public static event EventManager.CursedEventHandler<PlayerDisconnectedEventArgs> Disconnected;
    
    public static event EventManager.CursedEventHandler<PlayerChangingRoleEventArgs> ChangingRole;

    public static void OnPlayerJoined(PlayerJoinedEventArgs args)
    {
        Joined.InvokeEvent(args);
    }

    public static void OnPlayerDisconnected(PlayerDisconnectedEventArgs args)
    {
        Disconnected.InvokeEvent(args); 
        CursedLogger.InternalDebug("Removing player");
        CursedPlayer.Dictionary.Remove(args.Player.ReferenceHub);
    }

    public static void OnPlayerChangingRole(PlayerChangingRoleEventArgs args)
    {
        ChangingRole.InvokeEvent(args);
    }
}