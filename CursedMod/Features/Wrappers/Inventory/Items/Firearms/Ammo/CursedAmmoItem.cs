// -----------------------------------------------------------------------
// <copyright file="CursedAmmoItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Firearms.Ammo;

namespace CursedMod.Features.Wrappers.Inventory.Items.Firearms.Ammo;

public class CursedAmmoItem : CursedItem
{
    internal CursedAmmoItem(AmmoItem itemBase)
        : base(itemBase)
    {
        AmmoBase = itemBase;
    }
    
    public AmmoItem AmmoBase { get; }

    public int UnitPrice
    {
        get => AmmoBase.UnitPrice;
        set => AmmoBase.UnitPrice = value;
    }
}