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
        get => Base.TargetState;
        set => Base.NetworkTargetState = value;
    }

    public void TriggerState() => Base.NetworkTargetState = !IsOpen;

    public void ServerChangeLock(DoorLockReason reason, bool newState) => Base.ServerChangeLock(reason, newState);

    public override string ToString() =>
        $"{nameof(CursedDoor)}: Opened: {IsOpen} | Position: {Position} | Rotation: {Rotation} | Scale: {Scale} | Permissions: {RequiredPermissions} | DoorId: {DoorId}";
}