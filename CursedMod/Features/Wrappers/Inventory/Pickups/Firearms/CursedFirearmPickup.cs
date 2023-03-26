// -----------------------------------------------------------------------
// <copyright file="CursedFirearmPickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Firearms;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.Firearms;

public class CursedFirearmPickup : CursedPickup
{
    internal CursedFirearmPickup(FirearmPickup itemPickupBase)
        : base(itemPickupBase)
    {
        FirearmBase = itemPickupBase;
    }

    public FirearmPickup FirearmBase { get; }

    public FirearmStatus Status
    {
        get => FirearmBase.Status;
        set => FirearmBase.NetworkStatus = value;
    }
}