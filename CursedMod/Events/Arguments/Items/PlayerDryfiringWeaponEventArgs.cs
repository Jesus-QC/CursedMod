// -----------------------------------------------------------------------
// <copyright file="PlayerDryfiringWeaponEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Items.Firearms;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Firearms;

namespace CursedMod.Events.Arguments.Items;

public class PlayerDryfiringWeaponEventArgs : EventArgs, ICursedPlayerEvent, ICursedCancellableEvent
{
    public PlayerDryfiringWeaponEventArgs(ReferenceHub player, Firearm baseFirearm)
    {
        Player = CursedPlayer.Get(player);
        Weapon = CursedFirearmItem.Get(baseFirearm);
        IsAllowed = true;
    }
    
    public CursedFirearmItem Weapon { get; }
    
    public CursedPlayer Player { get; }
    
    public bool IsAllowed { get; set; } 
}