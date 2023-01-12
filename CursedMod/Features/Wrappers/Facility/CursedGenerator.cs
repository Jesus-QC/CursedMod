using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public class CursedGenerator
{
    public Scp079Generator Base { get; private set; } // todo: constructor

    public bool IsEngaged
    {
        get => Base.Engaged;
        set => Base.Engaged = value;
    }

    public bool IsActivating
    {
        get => Base.Activating;
        set => Base.Activating = value;
    }

    public int RemainingTime => Base.RemainingTime;
    
    public float DropdownSpeed => Base.DropdownSpeed;
    
    public Vector3 Position => Base.transform.position; //todo: setter
    
    public Quaternion Rotation => Base.transform.rotation; // todo: setter
    
    public Vector3 Scale => Base.transform.localScale; // todo: setter

    public KeycardPermissions RequiredPermissions
    {
        get => Base._requiredPermission;
        set => Base._requiredPermission = value;
    }

    public NetworkIdentity NetworkIdentity => Base.netIdentity;
    
    public override string ToString() => $"{nameof(CursedGenerator)}: Engaged {IsEngaged} | Activating: {IsActivating} | Remaining Time: {RemainingTime} | DropdownSpeed: {DropdownSpeed} | Position: {Position} | Rotation: {Rotation} | Scale: {Scale} | Permissions: {RequiredPermissions} | NetId: {NetworkIdentity.netId}";
}