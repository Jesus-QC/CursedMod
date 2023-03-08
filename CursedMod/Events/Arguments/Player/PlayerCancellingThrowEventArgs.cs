// -----------------------------------------------------------------------
// <copyright file="PlayerCancellingThrowEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Items;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items;

namespace CursedMod.Events.Arguments.Player;

public class PlayerCancellingThrowEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedItemEvent
{
    public PlayerCancellingThrowEventArgs(ItemBase throwableItem)
    {
        IsAllowed = true;
        Item = CursedItem.Get(throwableItem);
        Player = Item.Owner;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public CursedItem Item { get; }
}