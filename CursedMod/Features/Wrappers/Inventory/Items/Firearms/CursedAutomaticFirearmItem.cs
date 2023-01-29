// -----------------------------------------------------------------------
// <copyright file="CursedAutomaticFirearmItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CameraShaking;
using InventorySystem.Items.Firearms;

namespace CursedMod.Features.Wrappers.Inventory.Items.Firearms;

public class CursedAutomaticFirearmItem : CursedFirearmItem
{
    internal CursedAutomaticFirearmItem(AutomaticFirearm itemBase)
        : base(itemBase)
    {
        AutomaticFirearmBase = itemBase;
    }
    
    public AutomaticFirearm AutomaticFirearmBase { get; }

    public float FireRate
    {
        get => AutomaticFirearmBase._fireRate;
        set => AutomaticFirearmBase._fireRate = value;
    }

    public RecoilSettings RecoilSettings
    {
        get => AutomaticFirearmBase._recoil;
        set => AutomaticFirearmBase._recoil = value;
    }

    public FirearmRecoilPattern RecoilPattern
    {
        get => AutomaticFirearmBase._recoilPattern;
        set => AutomaticFirearmBase._recoilPattern = value;
    }
}