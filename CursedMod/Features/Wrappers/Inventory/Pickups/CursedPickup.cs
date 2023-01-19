using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Pickups;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Pickups;

public class CursedPickup
{
    public ItemPickupBase Base { get; }
    
    public GameObject GameObject { get; }
    
    public Transform Transform { get; }

    private CursedPickup(ItemPickupBase itemPickupBase)
    {
        Base = itemPickupBase;
        GameObject = Base.gameObject;
        Transform = Base._transform;
    }

    public static CursedPickup Create(ItemPickupBase pickupBase) => new CursedPickup(pickupBase);

    public PickupSyncInfo Info
    {
        get => Base.Info;
        set => Base.NetworkInfo = value;
    }
    
    public ushort Serial => Info.Serial;

    public ItemType ItemType
    {
        get => Info.ItemId;
        set => Info = new PickupSyncInfo(value, Info.Position, Info.Rotation, Info.Weight, Info.Serial);
    }
    
    public CursedPlayer PreviousOwner => CursedPlayer.Get(Base.PreviousOwner.Hub);


    public Vector3 Position
    {
        get => Info.Position;
        set => Info = new PickupSyncInfo(Info.ItemId, value, Info.Rotation, Info.Weight, Info.Serial);
    }

    public Quaternion Rotation
    {
        get => Info.Rotation;
        set => Info = new PickupSyncInfo(Info.ItemId, Info.Position, value, Info.Weight, Info.Serial);
    }

    public Vector3 Scale
    {
        get => Transform.localScale;
        set
        {
            Transform.localScale = value;
            CursedPlayer.SendSpawnMessageToAll(Base.netIdentity);
        }
    }

    public float Weight
    {
        get => Base.Info.Weight;
        set => Info = new PickupSyncInfo(Info.ItemId, Info.Position, Info.Rotation, value, Info.Serial);
    }

    public bool IsLocked
    {
        get => Base.Info.Locked;
        set
        {
            PickupSyncInfo info = Info;
            info.Locked = value;
            Info = info;
        }
    }

    public void Destroy() => Base.DestroySelf();
}