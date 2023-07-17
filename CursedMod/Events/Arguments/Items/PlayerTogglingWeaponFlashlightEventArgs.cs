// -----------------------------------------------------------------------
// <copyright file="PlayerTogglingWeaponFlashlightEventArgs.cs" company="CursedMod">
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

public class PlayerTogglingWeaponFlashlightEventArgs : EventArgs, ICursedPlayerEvent, ICursedCancellableEvent
{
    public PlayerTogglingWeaponFlashlightEventArgs(ReferenceHub player, Firearm baseFirearm, bool isEmittingLight)
    {
        Player = CursedPlayer.Get(player);
        Weapon = CursedFirearmItem.Get(baseFirearm);
        IsEmittingLight = isEmittingLight;
        IsAllowed = true;
    }
    
    public CursedFirearmItem Weapon { get; }
    
    public CursedPlayer Player { get; }
    
    public bool IsEmittingLight { get; set; }
    
    public bool IsAllowed { get; set; }
}