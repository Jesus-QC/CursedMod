// -----------------------------------------------------------------------
// <copyright file="Scp914UpgradedItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Pickups;
using InventorySystem.Items.Pickups;
using Scp914;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class Scp914UpgradedItemEventArgs : EventArgs, ICursedPickupEvent
{
    public Scp914UpgradedItemEventArgs(ItemPickupBase pickup, Vector3 outputPosition, Scp914KnobSetting knobSetting)
    {
        Pickup = CursedPickup.Get(pickup);
        OutputPosition = outputPosition;
        KnobSetting = knobSetting;
    }

    public CursedPickup Pickup { get; }

    public Vector3 OutputPosition { get; }
    
    public Scp914KnobSetting KnobSetting { get; }
}