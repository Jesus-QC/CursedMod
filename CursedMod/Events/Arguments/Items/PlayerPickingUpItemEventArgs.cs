// -----------------------------------------------------------------------
// <copyright file="PlayerPickingUpItemEventArgs.cs" company="CursedMod">
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

public class PlayerPickingUpItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedPickupEvent 
{
    public PlayerPickingUpItemEventArgs(SearchCompletor searchCompletor, ItemPickupBase pickupBase)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(searchCompletor.Hub);
        Pickup = CursedPickup.Get(pickupBase);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public CursedPickup Pickup { get; }

    public ItemType NewItemType
    {
        get => Pickup.ItemType;
        set => Pickup.ItemType = value;
    }
}