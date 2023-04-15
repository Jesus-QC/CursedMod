// -----------------------------------------------------------------------
// <copyright file="PlayerPickedUpItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Items;
using CursedMod.Features.Wrappers.Inventory.Pickups;
using CursedMod.Features.Wrappers.Player;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Searching;

namespace CursedMod.Events.Arguments.Items;

public class PlayerPickedUpItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedItemEvent 
{
    public PlayerPickedUpItemEventArgs(Inventory inventory, ItemBase itemBase)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(inventory._hub);
        Item = CursedItem.Get(itemBase);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public CursedItem Item { get; }
}