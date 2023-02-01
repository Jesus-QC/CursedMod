// -----------------------------------------------------------------------
// <copyright file="CursedAmmoPickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.Firearms.Ammo;

public class CursedAmmoPickup : CursedPickup
{
    internal CursedAmmoPickup(AmmoPickup itemPickupBase)
        : base(itemPickupBase)
    {
        AmmoBase = itemPickupBase;
    }
    
    public AmmoPickup AmmoBase { get; }

    public ushort SavedAmmo
    {
        get => AmmoBase.SavedAmmo;
        set => AmmoBase.NetworkSavedAmmo = value;
    }
}