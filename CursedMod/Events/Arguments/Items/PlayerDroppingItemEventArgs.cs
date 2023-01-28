// -----------------------------------------------------------------------
// <copyright file="PlayerDroppingItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Items;
using CursedMod.Features.Wrappers.Player;
using InventorySystem;
using InventorySystem.Items;

namespace CursedMod.Events.Arguments.Items;

public class PlayerDroppingItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedItemEvent 
{
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public CursedItem Item { get; }

    public bool IsThrow { get; set; }

    public PlayerDroppingItemEventArgs(Inventory inventory, ItemBase itemBase, bool isThrow)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(inventory._hub);
        Item = CursedItem.Get(itemBase);
        IsThrow = isThrow;
    }
}