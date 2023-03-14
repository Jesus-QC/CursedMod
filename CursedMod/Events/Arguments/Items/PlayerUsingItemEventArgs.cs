// -----------------------------------------------------------------------
// <copyright file="PlayerUsingItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Items.Usables;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Usables;

namespace CursedMod.Events.Arguments.Items;

public class PlayerUsingItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerUsingItemEventArgs(UsableItem usable)
    {
        IsAllowed = true;
        Item = CursedUsableItem.Get(usable);
        Player = Item.Owner;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public CursedUsableItem Item { get; }
}