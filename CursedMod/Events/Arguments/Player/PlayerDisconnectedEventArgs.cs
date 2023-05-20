// -----------------------------------------------------------------------
// <copyright file="PlayerDisconnectedEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using Mirror;
using PluginAPI.Core;

namespace CursedMod.Events.Arguments.Player;

public class PlayerDisconnectedEventArgs : EventArgs, ICursedPlayerEvent
{
    public PlayerDisconnectedEventArgs(ReferenceHub hub)
    {
        Player = CursedPlayer.Get(hub);
    }
    
    public CursedPlayer Player { get; }
}