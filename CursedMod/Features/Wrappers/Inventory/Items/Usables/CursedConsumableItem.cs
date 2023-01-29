// -----------------------------------------------------------------------
// <copyright file="CursedConsumableItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Usables;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables;

public class CursedConsumableItem : CursedUsableItem
{
    internal CursedConsumableItem(Consumable itemBase)
        : base(itemBase)
    {
        BaseConsumable = itemBase;
    }
    
    public Consumable BaseConsumable { get; }

    public bool ActivationReady => BaseConsumable.ActivationReady;

    public void Consume() => BaseConsumable.ActivateEffects();
}