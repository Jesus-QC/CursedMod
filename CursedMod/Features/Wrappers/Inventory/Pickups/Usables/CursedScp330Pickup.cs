// -----------------------------------------------------------------------
// <copyright file="CursedScp330Pickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.Usables;

public class CursedScp330Pickup : CursedPickup
{
    internal CursedScp330Pickup(Scp330Pickup itemPickupBase)
        : base(itemPickupBase)
    {
        Scp330Base = itemPickupBase;
    }
    
    public Scp330Pickup Scp330Base { get; }

    public CandyKindID ExposedCandy
    {
        get => Scp330Base.ExposedCandy;
        set => Scp330Base.NetworkExposedCandy = value;
    }

    public List<CandyKindID> StoredCandies
    {
        get => Scp330Base.StoredCandies;
        set => Scp330Base.StoredCandies = value;
    }
}