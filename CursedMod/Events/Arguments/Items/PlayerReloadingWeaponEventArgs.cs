// -----------------------------------------------------------------------
// <copyright file="PlayerReloadingWeaponEventArgs.cs" company="CursedMod">
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

public class PlayerReloadingWeaponEventArgs : EventArgs, ICursedPlayerEvent, ICursedCancellableEvent
{
    public PlayerReloadingWeaponEventArgs(ReferenceHub player, Firearm baseFirearm)
    {
        Player = CursedPlayer.Get(player);
        Weapon = CursedFirearmItem.Get(baseFirearm);
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }
    
    public CursedFirearmItem Weapon { get; }

    public bool IsAllowed { get; set; }
}