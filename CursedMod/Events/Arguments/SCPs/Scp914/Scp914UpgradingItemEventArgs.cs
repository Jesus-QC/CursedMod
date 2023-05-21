// -----------------------------------------------------------------------
// <copyright file="Scp914UpgradingItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CursedMod.Features.Wrappers.Inventory.Pickups;
using InventorySystem.Items.Pickups;
using Scp914;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class Scp914UpgradingItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPickupEvent
{
    public Scp914UpgradingItemEventArgs(ItemPickupBase pickup, Vector3 outputPosition, Scp914KnobSetting knobSetting)
    {
        IsAllowed = true;
        Pickup = CursedPickup.Get(pickup);
        OutputPosition = outputPosition;
        KnobSetting = knobSetting;
    }

    public bool IsAllowed { get; set; }

    public CursedPickup Pickup { get; }

    public Vector3 OutputPosition { get; set; }
    
    public Scp914KnobSetting KnobSetting { get; set; }
}