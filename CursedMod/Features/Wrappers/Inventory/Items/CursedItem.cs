using CursedMod.Features.Wrappers.Inventory.Items.Armor;
using CursedMod.Features.Wrappers.Inventory.Items.Firearms;
using CursedMod.Features.Wrappers.Player;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Armor;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Thirdperson;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Items;

public class CursedItem
{
    public ItemBase Base { get; }

    public GameObject GameObject { get; }

    public Transform Transform { get; }

    internal CursedItem(ItemBase itemBase)
    {
        Base = itemBase;
        GameObject = itemBase.gameObject;
        Transform = itemBase.transform;
    }

    public static CursedItem Get(ItemBase itemBase)
    {
        return itemBase switch
        {
            Firearm firearm => CursedFirearmItem.Get(itemBase),
            BodyArmor bodyArmor => new CursedBodyArmorItem(bodyArmor),
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
    
    public CursedPlayer Owner => CursedPlayer.Get(Base.Owner);

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

    public void HoldItem() => Owner.CurrentItem = this;

    public void Drop() => Base.ServerDropItem();
    
    public bool CanHolster() => Base.CanHolster();

    public bool CanEquip() => Base.CanEquip();

    public void DestroyInstance() => Base.OwnerInventory.DestroyItemInstance(Serial, null, out _);
}
