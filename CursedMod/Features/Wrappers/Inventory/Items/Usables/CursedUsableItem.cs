// -----------------------------------------------------------------------
// <copyright file="CursedUsableItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Usables;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables;

public class CursedUsableItem : CursedItem
{
    internal CursedUsableItem(UsableItem itemBase)
        : base(itemBase)
    {
        UsableBase = itemBase;
    }
    
    public UsableItem UsableBase { get; }

    public bool CanStartUsing
    {
        get => UsableBase.CanStartUsing;
        set => UsableBase.CanStartUsing = value;
    }

    public bool IsUsing
    {
        get => UsableBase.IsUsing;
        set => UsableBase.IsUsing = value;
    }

    public float RemainingCooldown
    {
        get => UsableBase.RemainingCooldown;
        set => UsableBase.RemainingCooldown = value;
    }

    public float UseTime
    {
        get => UsableBase.UseTime;
        set => UsableBase.UseTime = value;
    }

    public float MaxCancellableTime
    {
        get => UsableBase.MaxCancellableTime;
        set => UsableBase.MaxCancellableTime = value;
    }
    
    public static CursedUsableItem Get(UsableItem item)
    {
        if (item is Consumable consumable)
            return new CursedConsumableItem(consumable);

        return new CursedUsableItem(item);
    }
    
    public void SetPersonalCooldown(float seconds) => UsableBase.ServerSetPersonalCooldown(seconds);
    
    public void SetGlobalCooldown(float seconds) => UsableBase.ServerSetGlobalItemCooldown(seconds);
}