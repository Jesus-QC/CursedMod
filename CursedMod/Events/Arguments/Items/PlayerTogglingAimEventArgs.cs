// -----------------------------------------------------------------------
// <copyright file="PlayerTogglingAimEventArgs.cs" company="CursedMod">
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

public class PlayerTogglingAimEventArgs : EventArgs, ICursedPlayerEvent
{
    public PlayerTogglingAimEventArgs(ReferenceHub player, Firearm baseFirearm, bool isAiming)
    {
        Player = CursedPlayer.Get(player);
        Weapon = CursedFirearmItem.Get(baseFirearm);
        IsAiming = isAiming;
    }
    
    public CursedFirearmItem Weapon { get; }
    
    public CursedPlayer Player { get; }
    
    public bool IsAiming { get; }
}