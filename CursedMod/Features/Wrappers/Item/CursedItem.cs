using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Item;

public class CursedItem
{
    public ItemBase Base { get; }

    public CursedItem(ItemBase itemBase)
    {
        Base = itemBase;
    }

    public ItemType ItemType => Base.ItemTypeId;

    public ushort Serial => Base.ItemSerial;

    public float Weight => Base.Weight;

    public CursedPlayer Owner => Base.Owner == null ? null : CursedPlayer.Get(Base.Owner);

    public GameObject GameObject => Base.gameObject;

    public Transform Transform => Base.transform;

    public Vector3 Podition
    {
        get => Transform.position;
        set => Transform.position = value;
    }

    public Quaternion Rotation
    {
        get => Transform.rotation;
        set => Transform.rotation = value;
    }

    public Vector3 Scale
    {
        get => Transform.localScale;
        set => Transform.localScale = value;
    }
}
