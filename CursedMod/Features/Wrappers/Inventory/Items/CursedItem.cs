using CursedMod.Features.Wrappers.Player;
using InventorySystem;
using InventorySystem.Items;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Items;

public class CursedItem
{
    public ItemBase Base { get; }

    public GameObject GameObject { get; }

    public Transform Transform { get; }

    private CursedItem(ItemBase itemBase)
    {
        Base = itemBase;
        GameObject = itemBase.gameObject;
        Transform = itemBase.transform;
    }

    public static CursedItem Get(ItemBase itemBase) => new (itemBase);

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

    public ItemType ItemType => Base.ItemTypeId;

    public ushort Serial => Base.ItemSerial;

    public float Weight => Base.Weight;

    public CursedPlayer Owner => CursedPlayer.Get(Base.Owner);

    public void HoldItem() => Owner.CurrentItem = this;
}
