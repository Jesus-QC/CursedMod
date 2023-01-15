using System;
using System.Linq;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;
using DoorSpawnpoint = MapGeneration.DoorSpawnpoint;

namespace CursedMod.Features.Wrappers.Facility.Rooms.Doors;

public class CursedDoor
{
    public DoorVariant Base { get; }
    
    public CursedDoor(DoorVariant door)
    {
        Base = door;
    }

    public GameObject GameObject => Base.gameObject;

    public Transform Transform => Base.transform;

    public int DoorId => Base.DoorId;
    
    public float State => Base.GetExactState();
    
    public bool IsMoving => State is not(0 or 1);
    
    public bool IsClosed => !Base.TargetState;
    
    public bool IsOpened => Base.TargetState;

    public Vector3 Position
    {
        get => Base.transform.position;
        set => Base.transform.position = value;
    }

    public Quaternion Rotation
    {
        get => Base.transform.rotation;
        set => Base.transform.rotation = value;
    }

    public Vector3 Scale
    {
        get => Base.transform.localScale;
        set => Base.transform.localScale = value;
    }

    public DoorPermissions RequiredPermissions
    {
        get => Base.RequiredPermissions;
        set => Base.RequiredPermissions = value;
    }

    public bool IsConsideredOpened
    {
        get => Base.IsConsideredOpen();
        set => Base.NetworkTargetState = value;
    }
    
    public bool IsGate => Base is PryableDoor;
    
    public bool IsCheckpoint => Base is CheckpointDoor;
    
    public bool IsElevator => Base is ElevatorDoor;

    public bool IsDamageable => Base is IDamageableDoor;
    
    public bool IsBroken => Base is IDamageableDoor damageable && damageable.IsDestroyed;
    
    public DoorNametagExtension Tag => Base.GetComponent<DoorNametagExtension>();

    public void TriggerState() => Base.NetworkTargetState = !IsOpened;

    public void ServerChangeLock(DoorLockReason reason, bool newState) => Base.ServerChangeLock(reason, newState);
    
    public bool DestroyDoor(DoorDamageType type = DoorDamageType.ServerCommand)
    {
        if (Base is IDamageableDoor damageable && !damageable.IsDestroyed)
        {
            damageable.ServerDamage(ushort.MaxValue, type);
            return true;
        }

        return false;
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

    public override string ToString() => $"{nameof(CursedDoor)}: Opened: {IsOpened} | Position: {Position} | Rotation: {Rotation} | Scale: {Scale} | Permissions: {RequiredPermissions} | DoorId: {DoorId}";
}
