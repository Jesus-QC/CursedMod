// -----------------------------------------------------------------------
// <copyright file="PlayerDisconnectingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using Mirror;

namespace CursedMod.Events.Arguments.Player;

public class PlayerDisconnectingEventArgs : EventArgs, ICursedPlayerEvent
{
    public PlayerDisconnectingEventArgs(NetworkConnection connection)
    {
        Player = CursedPlayer.Get(connection.identity);
    }
    
    public CursedPlayer Player { get; }
}