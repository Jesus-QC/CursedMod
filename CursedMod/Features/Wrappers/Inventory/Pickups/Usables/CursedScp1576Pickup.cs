// -----------------------------------------------------------------------
// <copyright file="CursedScp1576Pickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Usables.Scp1576;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.Usables;

public class CursedScp1576Pickup : CursedPickup
{
    internal CursedScp1576Pickup(Scp1576Pickup itemPickupBase)
        : base(itemPickupBase)
    {
        Scp1576Base = itemPickupBase;
    }
    
    public Scp1576Pickup Scp1576Base { get; }

    public byte Horn
    {
        get => Scp1576Base._syncHorn;
        set => Scp1576Base.Network_syncHorn = value;
    }

    public byte PreviousHorn
    {
        get => Scp1576Base._prevSyncHorn;
        set => Scp1576Base._prevSyncHorn = value;
    }
}