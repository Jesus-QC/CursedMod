// -----------------------------------------------------------------------
// <copyright file="CursedDoor.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
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
    public static readonly Dictionary<DoorVariant, CursedDoor> Dictionary = new ();

    internal CursedDoor(DoorVariant door)
    {
        Dictionary.Add(door, this);
        
        Base = door;
        GameObject = door.gameObject;
        Transform = door.transform;
        Tag = Base.GetComponent<DoorNametagExtension>();
        DoorType = DoorType.Basic;
    }
    
    public static IEnumerable<CursedDoor> Collection => Dictionary.Values;
    
    public static IEnumerable<CursedDoor> List => Dictionary.Values.ToList();
    
    public DoorVariant Base { get; }
    
    public GameObject GameObject { get; }
    
    public Transform Transform { get; }
    
    public DoorNametagExtension Tag { get; }
    
    public DoorType DoorType { get; internal set; }

    public int DoorId => Base.DoorId;
    
    public float State => Base.GetExactState();
    
    public bool IsMoving => State is not(0 or 1);

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
    
    public bool IsCheckpointDoor => Base is CheckpointDoor;
    
    public bool IsElevatorDoor => Base is ElevatorDoor;

    public bool IsDamageable => Base is IDamageableDoor;
    
    public bool IsDestroyed => Base is IDamageableDoor { IsDestroyed: true };

    public static CursedDoor Get(DoorVariant doorVariant)
    {
        if (Dictionary.TryGetValue(doorVariant, out CursedDoor value))
            return value;
        
        return doorVariant switch
        {
            BreakableDoor breakableDoor => new CursedBreakableDoor(breakableDoor),
            PryableDoor pryableDoor => new CursedPryableDoor(pryableDoor),
            CheckpointDoor checkpointDoor => new CursedCheckpointDoor(checkpointDoor),
            _ => new CursedDoor(doorVariant)
        };
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

        return Get(door);
    }

    public void TriggerState() => IsOpened = !IsOpened;
    
    public void Open() => IsOpened = true;
    
    public void Close() => IsClosed = true;
    
    public void HasPerms(ItemBase item) => RequiredPermissions.CheckPermissions(item, null);
    
    public void ChangeLock(DoorLockReason reason, bool newState) => Base.ServerChangeLock(reason, newState);

    public void Lock() => Base.ServerChangeLock(DoorLockReason.AdminCommand, true);

    public void Unlock() => Base.ServerChangeLock(DoorLockReason.AdminCommand, false);

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
    
    internal static void CacheAllDoors()
    {
        foreach (DoorVariant door in DoorVariant.AllDoors)
        {
            Get(door);
        }   
    }
}
