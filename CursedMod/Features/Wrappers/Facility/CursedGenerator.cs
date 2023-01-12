using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedGenerator
{
    public static Scp079Generator Base { get; private set; }

    public static bool IsEngaged
    {
        get => Base.Engaged;
        set => Base.Engaged = value;
    }

    public static bool IsActivating
    {
        get => Base.Activating;
        set => Base.Activating = value;
    }

    public static int RemainingTime => Base.RemainingTime;
    
    public static float DropdownSpeed => Base.DropdownSpeed;
    
    public static Vector3 Position => Base.transform.position;
    
    public static Quaternion Rotation => Base.transform.rotation;
    
    public static Vector3 Scale => Base.transform.localScale;

    public static KeycardPermissions RequiredPermissions
    {
        get => Base._requiredPermission;
        set => Base._requiredPermission = value;
    }

    public static NetworkIdentity NetworkIdentity => Base.netIdentity;
}