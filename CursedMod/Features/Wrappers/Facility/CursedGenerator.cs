using System;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public class CursedGenerator
{
    public Scp079Generator Base { get; }
    
    public CursedGenerator(Scp079Generator generator)
    {
        Base = generator;
    }

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

    public Vector3 Position
    {
        get => Base.transform.position;
        set
        {
            Base.transform.position = value;
            CursedPlayer.SendSpawnMessageToAll(NetworkIdentity);
        }
    }

    public Quaternion Rotation
    {
        get => Base.transform.rotation;
        set
        {
            Base.transform.rotation = value;
            CursedPlayer.SendSpawnMessageToAll(NetworkIdentity);
        }
    }

    public Vector3 Scale
    {
        get => Base.transform.localScale;
        set
        {
            Base.transform.localScale = value;
            CursedPlayer.SendSpawnMessageToAll(NetworkIdentity);
        }
    }

    public KeycardPermissions RequiredPermissions
    {
        get => Base._requiredPermission;
        set => Base._requiredPermission = value;
    }

    public NetworkIdentity NetworkIdentity => Base.netIdentity;

    public override string ToString() =>
        $"{nameof(CursedGenerator)}: Engaged {IsEngaged} | Activating: {IsActivating} | Remaining Time: {RemainingTime} | DropdownSpeed: {DropdownSpeed} | Position: {Position} | Rotation: {Rotation} | Scale: {Scale} | Permissions: {RequiredPermissions} | NetId: {NetworkIdentity.netId}";
}