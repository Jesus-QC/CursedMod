using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory;

public class CursedItem
{
    public ItemBase Base { get; }

    public GameObject GameObject { get; }

    public Transform Transform { get; }
    
    public CursedItem(ItemBase itemBase)
    {
        Base = itemBase;
        GameObject = itemBase.gameObject;
        Transform = itemBase.transform;
    }

    public ItemType ItemType => Base.ItemTypeId;

    public ushort Serial => Base.ItemSerial;

    public float Weight => Base.Weight;

    public CursedPlayer Owner => CursedPlayer.Get(Base.Owner);
}
