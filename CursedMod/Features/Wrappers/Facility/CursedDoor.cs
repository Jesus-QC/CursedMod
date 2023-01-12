using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

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
    
    public bool FullClosed => State is 0;
    
    public bool FullOpened => State is 1;

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

    public bool IsOpen
    {
        get => Base.IsConsideredOpen();
        set => Base.NetworkTargetState = value;
    }
    
    public bool IsGate => Base is PryableDoor;
    
    public bool IsCheckpoint => Base is CheckpointDoor;
    
    public bool IsElevator => Base is ElevatorDoor;
    
    public bool IsBroken => Base is IDamageableDoor damageable && damageable.IsDestroyed;
    
    public DoorNametagExtension Tag => Base.GetComponent<DoorNametagExtension>();

    public void TriggerState() => Base.NetworkTargetState = !IsOpen;

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

    public override string ToString() =>
        $"{nameof(CursedDoor)}: Opened: {IsOpen} | Position: {Position} | Rotation: {Rotation} | Scale: {Scale} | Permissions: {RequiredPermissions} | DoorId: {DoorId}";
}