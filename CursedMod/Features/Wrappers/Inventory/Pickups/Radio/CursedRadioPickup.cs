// -----------------------------------------------------------------------
// <copyright file="CursedRadioPickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Radio;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.Radio;

public class CursedRadioPickup : CursedPickup
{
    internal CursedRadioPickup(RadioPickup itemPickupBase)
        : base(itemPickupBase)
    {
        RadioBase = itemPickupBase;
    }
    
    public RadioPickup RadioBase { get; }

    public bool RadioSavedEnabled
    {
        get => RadioBase.SavedEnabled;
        set => RadioBase.NetworkSavedEnabled = value;
    }

    public byte RadioSavedRange
    {
        get => RadioBase.SavedRange;
        set => RadioBase.NetworkSavedRange = value;
    }

    public float RadioSavedBattery
    {
        get => RadioBase.SavedBattery;
        set => RadioBase.SavedBattery = value;
    }
}