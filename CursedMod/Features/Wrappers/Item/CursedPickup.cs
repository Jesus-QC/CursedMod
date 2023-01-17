using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Pickups;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Item;

public class CursedPickup
{
    public ItemPickupBase Base { get; }

    public CursedPickup(ItemPickupBase ItemPickupBase)
    {
        Base = ItemPickupBase;
    }

    public PickupSyncInfo Info => Base.Info;

    public ItemType ItemType => Info.ItemId;

    public ushort Serial => Info.Serial;

    public CursedPlayer PreviousOwner => Base.PreviousOwner.Hub == null ? null : CursedPlayer.Get(Base.PreviousOwner.Hub);

    public GameObject GameObject => Base.gameObject;
    
    public Transform Transform => Base.transform; 

    public Vector3 Position => Transform.position;

    public Quaternion Rotation => Transform.rotation;

    public Vector3 Scale => Transform.localScale;

    public float Weight
    {
        get => Base.Info.Weight;
        set => Base.Info.Weight = value;
    }

    public bool IsLocked
    {
        get => Base.Info.Locked;
        set => Base.Info.Locked = value;
    }

    public void Destroy() => Base.DestroySelf();
}