using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Server;
using InventorySystem;
using InventorySystem.Items.Pickups;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Pickups;

public class CursedPickup
{
    public ItemPickupBase Base { get; }
    
    public GameObject GameObject { get; }
    
    public Transform Transform { get; }
    
    public Rigidbody Rigidbody { get; }

    private CursedPickup(ItemPickupBase itemPickupBase)
    {
        Base = itemPickupBase;
        GameObject = Base.gameObject;
        Transform = Base._transform;
        Rigidbody = Base.RigidBody;
    }

    public static CursedPickup Get(ItemPickupBase pickupBase) => new(pickupBase);

    public static CursedPickup Create(ItemType type, PickupSyncInfo pickupSyncInfo, bool spawn = true) 
        => Get(CursedServer.LocalPlayer.Inventory.ServerCreatePickup(CursedServer.LocalPlayer.AddItemBase(type), pickupSyncInfo, spawn));

    public static CursedPickup Create(ItemType type, bool spawn = true) 
        => Create(type, PickupSyncInfo.None);

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

    public GameObject Spawn()
    {
        NetworkServer.Spawn(GameObject);
        return GameObject;
    }

    public void UnSpawn()
    {
        NetworkServer.UnSpawn(GameObject);
    }
    
    public void Destroy() => Base.DestroySelf();
}