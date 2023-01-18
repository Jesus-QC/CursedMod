using CursedMod.Features.Enums;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using MapGeneration;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers.Facility.Doors;

public class CursedDoor
{
    public DoorVariant Base { get; }
    
    public GameObject GameObject { get; }
    
    public Transform Transform { get; }
    
    public DoorNametagExtension Tag { get; }
    
    public DoorType DoorType { get; internal set; }
    
    internal CursedDoor(DoorVariant door)
    {
        Base = door;
        GameObject = door.gameObject;
        Transform = door.transform;
        Tag = Base.GetComponent<DoorNametagExtension>();
        DoorType = DoorType.Basic;
    }

    public static CursedDoor Get(DoorVariant doorVariant)
    {
        if (doorVariant is BreakableDoor breakableDoor)
            return new CursedBreakableDoor(breakableDoor);
        if (doorVariant is PryableDoor pryableDoor)
            return new CursedPryableDoor(pryableDoor);
        if (doorVariant is CheckpointDoor checkpointDoor)
            return new CursedCheckpointDoor(checkpointDoor);

        return new CursedDoor(doorVariant);
    }

    public int DoorId => Base.DoorId;
    
    public float State => Base.GetExactState();
    
    public bool IsMoving => State is not (0 or 1);

    public bool IsClosed
    {
        get => !Base.TargetState;
        set => Base.NetworkTargetState = !value;
    }

    public bool IsOpened
    {
        get => Base.TargetState;
        set => Base.NetworkTargetState = value;
    }

    public bool IsConsideredOpened => Base.IsConsideredOpen();
    
    public DoorPermissions RequiredPermissions
    {
        get => Base.RequiredPermissions;
        set => Base.RequiredPermissions = value;
    }
    
    public Vector3 Position
    {
        get => Base.transform.position;
        set
        {
            Transform.position = value;
            CursedPlayer.SendSpawnMessageToAll(Base.netIdentity);
        }
    }

    public Quaternion Rotation
    {
        get => Transform.rotation;
        set 
        {
            Transform.rotation = value;
            CursedPlayer.SendSpawnMessageToAll(Base.netIdentity);
        }
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

    public string Name
    {
        get => Tag.GetName;
        set => Tag.UpdateName(value);
    }

    public bool IsGate => Base is PryableDoor;
    
    public bool IsCheckpoint => Base is CheckpointDoor;
    
    public bool IsElevator => Base is ElevatorDoor;

    public bool IsDamageable => Base is IDamageableDoor;
    
    public bool IsBroken => Base is IDamageableDoor { IsDestroyed: true };

    public void TriggerState() => IsOpened = !IsOpened;
    
    public void Open() => IsOpened = true;
    
    public void Close() => IsClosed = true;
    
    public void HasPerms(ItemBase item) => RequiredPermissions.CheckPermissions(item, null);
    
    public void ChangeLock(DoorLockReason reason, bool newState) => Base.ServerChangeLock(reason, newState);

    public void Lock() => Base.ServerChangeLock(DoorLockReason.AdminCommand, true);

    public void Unlock() => Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
    
    public bool TryToPryGate() => this is CursedPryableDoor pr && pr.TryPry();
    
    public bool TryToDamage(float damage, DoorDamageType damageType) => Base is BreakableDoor br && br.ServerDamage(damage, damageType);

    public bool TryToBreak() => Base is BreakableDoor br && br.ServerDamage(br.RemainingHealth, DoorDamageType.ServerCommand);

    public bool DestroyDoor(DoorDamageType type = DoorDamageType.ServerCommand)
    {
        if (Base is not IDamageableDoor damageable || damageable.IsDestroyed) 
            return false;
        
        damageable.ServerDamage(ushort.MaxValue, type);
        return true;

    }

    public static CursedDoor Create(FacilityZone doorType, Vector3 position, Vector3 rotation, Vector3? scale = null, bool spawn = false)
    {
        DoorVariant prefab = doorType switch
        {
            FacilityZone.HeavyContainment => CursedPrefabManager.HczDoor,
            FacilityZone.LightContainment => CursedPrefabManager.LczDoor,
            FacilityZone.Entrance => CursedPrefabManager.EzDoor,
            FacilityZone.Surface => CursedPrefabManager.LczDoor,
            FacilityZone.Other => CursedPrefabManager.LczDoor,
            FacilityZone.None => CursedPrefabManager.LczDoor,
            _ => CursedPrefabManager.LczDoor,
        };

        DoorVariant door = Object.Instantiate(prefab, position, Quaternion.Euler(rotation));

        if (scale.HasValue)
            door.transform.localScale = scale.Value;

        if (spawn)
            NetworkServer.Spawn(door.gameObject);

        return new CursedDoor(door);
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

    public override string ToString() => $"{nameof(CursedDoor)}: Opened: {IsOpened} | Position: {Position} | Rotation: {Rotation} | Scale: {Scale} | Permissions: {RequiredPermissions} | DoorId: {DoorId}";
}
