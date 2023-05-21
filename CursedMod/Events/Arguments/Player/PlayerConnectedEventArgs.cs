// -----------------------------------------------------------------------
// <copyright file="PlayerConnectedEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.Player;

public class PlayerConnectedEventArgs : EventArgs, ICursedPlayerEvent
{
    public PlayerConnectedEventArgs(ServerRoles serverRoles)
    {
        Player = new CursedPlayer(serverRoles._hub);
    }
    
    public CursedPlayer Player { get; }
}