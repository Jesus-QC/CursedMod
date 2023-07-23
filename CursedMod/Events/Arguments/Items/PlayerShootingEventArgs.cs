// -----------------------------------------------------------------------
// <copyright file="PlayerShootingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Firearms.BasicMessages;
using Mirror;

namespace CursedMod.Events.Arguments.Items;

public class PlayerShootingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerShootingEventArgs(NetworkConnection connection, ShotMessage shotMessage)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(connection.identity);
        ShotMessage = shotMessage;
    }

    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public ShotMessage ShotMessage { get; }
}