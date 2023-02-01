// -----------------------------------------------------------------------
// <copyright file="CursedMicroHIDPickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.MicroHID;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.MicroHID;

public class CursedMicroHidPickup : CursedPickup
{
    internal CursedMicroHidPickup(MicroHIDPickup itemPickupBase)
        : base(itemPickupBase)
    {
        MicroHidBase = itemPickupBase;
    }
    
    public MicroHIDPickup MicroHidBase { get; }

    public float Energy
    {
        get => MicroHidBase.Energy;
        set => MicroHidBase.NetworkEnergy = value;
    }
}