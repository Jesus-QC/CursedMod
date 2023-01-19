using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items;
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

    public static CursedItem Get(ItemBase itemBase) => new (itemBase);

    public ItemType ItemType => Base.ItemTypeId;

    public ushort Serial => Base.ItemSerial;

    public float Weight => Base.Weight;

    public CursedPlayer Owner => CursedPlayer.Get(Base.Owner);

    public void HoldItem() => Owner.CurrentItem = this;
}
