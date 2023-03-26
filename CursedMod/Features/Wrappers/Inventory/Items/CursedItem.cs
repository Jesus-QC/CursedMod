// -----------------------------------------------------------------------
// <copyright file="CursedItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Inventory.Items.Armor;
using CursedMod.Features.Wrappers.Inventory.Items.Firearms;
using CursedMod.Features.Wrappers.Inventory.Items.Firearms.Ammo;
using CursedMod.Features.Wrappers.Inventory.Items.Flashlight;
using CursedMod.Features.Wrappers.Inventory.Items.Jailbird;
using CursedMod.Features.Wrappers.Inventory.Items.KeyCards;
using CursedMod.Features.Wrappers.Inventory.Items.Radio;
using CursedMod.Features.Wrappers.Inventory.Items.ThrowableProjectiles;
using CursedMod.Features.Wrappers.Inventory.Items.Usables;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Server;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Armor;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Flashlight;
using InventorySystem.Items.Jailbird;
using InventorySystem.Items.Keycards;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Radio;
using InventorySystem.Items.Thirdperson;
using InventorySystem.Items.ThrowableProjectiles;
using InventorySystem.Items.Usables;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Items;

public class CursedItem
{
    internal CursedItem(ItemBase itemBase)
    {
        Base = itemBase;
        GameObject = itemBase.gameObject;
        Transform = itemBase.transform;
    }
    
    public ItemBase Base { get; }

    public GameObject GameObject { get; }

    public Transform Transform { get; }
    
    public CursedPlayer Owner => CursedPlayer.Get(Base.Owner);
    
    public ItemType ItemType => Base.ItemTypeId;

    public ItemCategory Category => Base.Category;

    public ItemTierFlags TierFlags => Base.TierFlags;

    public ThirdpersonItemBase ThirdPersonModel
    {
        get => Base.ThirdpersonModel;
        set => Base.ThirdpersonModel = value;
    }

    public ItemThrowSettings ThrowSettings
    {
        get => Base.ThrowSettings;
        set => Base.ThrowSettings = value;
    }

    public ItemPickupBase PickupDropModel
    {
        get => Base.PickupDropModel;
        set => Base.PickupDropModel = value;
    }

    public ItemDescriptionType DescriptionType
    {
        get => Base.DescriptionType;
        set => Base.DescriptionType = value;
    }

    public ushort Serial => Base.ItemSerial;

    public float Weight => Base.Weight;

    public static CursedItem Get(ItemBase itemBase)
    {
        return itemBase switch
        {
            Firearm firearm => CursedFirearmItem.Get(firearm),
            BodyArmor bodyArmor => new CursedBodyArmorItem(bodyArmor),
            AmmoItem ammoItem => new CursedAmmoItem(ammoItem),
            KeycardItem keyCardItem => new CursedKeyCardItem(keyCardItem),
            RadioItem radioItem => new CursedRadioItem(radioItem),
            ThrowableItem throwableItem => new CursedThrowableItem(throwableItem),
            UsableItem usableItem => CursedUsableItem.Get(usableItem),
            FlashlightItem flashlightItem => new CursedFlashlightItem(flashlightItem),
            JailbirdItem jailbirdItem => new CursedJailbirdItem(jailbirdItem),
            _ => new CursedItem(itemBase)
        };
    }

    public static bool TryGetWithSerial(ushort serial, out CursedItem item)
    {
        if (InventoryExtensions.ServerTryGetItemWithSerial(serial, out ItemBase itemBase))
        {
            item = Get(itemBase);
            return true;
        }

        item = null;
        return false;
    }

    public static bool TryGetOwnerWithSerial(ushort serial, out CursedPlayer owner)
    {
        if (InventoryExtensions.TryGetHubHoldingSerial(serial, out ReferenceHub hub))
        {
            owner = CursedPlayer.Get(hub);
            return true;
        }

        owner = null;
        return false;
    }
    
    public static CursedItem Create(ItemType type) => Get(CursedServer.LocalPlayer.Inventory.CreateItemInstance(new ItemIdentifier(type, ItemSerialGenerator.GenerateNext()), false));

    public void HoldItem() => Owner.CurrentItem = this;

    public void Drop() => Base.ServerDropItem();
    
    public bool CanHolster() => Base.CanHolster();

    public bool CanEquip() => Base.CanEquip();

    public void DestroyInstance() => Base.OwnerInventory.DestroyItemInstance(Serial, null, out _);
}
