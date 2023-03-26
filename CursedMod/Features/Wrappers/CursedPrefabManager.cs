// -----------------------------------------------------------------------
// <copyright file="CursedPrefabManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using AdminToys;
using CursedMod.Features.Enums;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using MapGeneration.Distributors;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers;

public static class CursedPrefabManager
{
    private static readonly Dictionary<LockerType, Locker> LockerStructures = new ();
    private static PrimitiveObjectToy _primitiveObjectToy;
    private static LightSourceToy _lightSourceToy;
    private static ShootingTarget _sportShootingTarget;
    private static ShootingTarget _dBoyShootingTarget;
    private static ShootingTarget _binaryShootingTarget;
    private static DoorVariant _lczDoor;
    private static DoorVariant _hczDoor;
    private static DoorVariant _ezDoor;

    public static PrimitiveObjectToy PrimitiveObject
    {
        get
        {
            if (_primitiveObjectToy is not null)
                return _primitiveObjectToy;

            TryGetPrefabOfType(out _primitiveObjectToy);
            return _primitiveObjectToy;
        }
    }
    
    public static LightSourceToy LightSource
    {
        get
        {
            if (_lightSourceToy is not null)
                return _lightSourceToy;
 
            TryGetPrefabOfType(out _lightSourceToy);
            return _lightSourceToy;
        }
    }
    
    public static ShootingTarget SportShootingTarget
    {
        get
        {
            if (_sportShootingTarget is not null)
                return _sportShootingTarget;
 
            TryGetPrefabOfType("sportTargetPrefab", out _sportShootingTarget);
            return _sportShootingTarget;
        }
    }
    
    public static ShootingTarget DBoyShootingTarget
    {
        get
        {
            if (_dBoyShootingTarget is not null)
                return _dBoyShootingTarget;
            
            TryGetPrefabOfType("dboyTargetPrefab", out _dBoyShootingTarget);
            return _dBoyShootingTarget;
        }
    }
    
    public static ShootingTarget BinaryShootingTarget
    {
        get
        {
            if (_binaryShootingTarget is not null)
                return _binaryShootingTarget;
            
            TryGetPrefabOfType("binaryTargetPrefab", out _binaryShootingTarget);
            return _binaryShootingTarget;
        }
    }
    
    public static DoorVariant LczDoor
    {
        get
        {
            if (_lczDoor is not null)
                return _lczDoor;

            DoorVariant door = Object.FindObjectsOfType<DoorSpawnpoint>().First(x => x.TargetPrefab.name.Contains("LCZ")).TargetPrefab;
            _lczDoor = door;
            return door;
        }
    }
    
    public static DoorVariant HczDoor
    {
        get
        {
            if (_hczDoor is not null)
                return _hczDoor;

            DoorVariant door = Object.FindObjectsOfType<DoorSpawnpoint>().First(x => x.TargetPrefab.name.Contains("HCZ")).TargetPrefab;
            _hczDoor = door;
            return door;
        }
    }
    
    public static DoorVariant EzDoor
    {
        get
        {
            if (_ezDoor is not null)
                return _ezDoor;

            DoorVariant door = Object.FindObjectsOfType<DoorSpawnpoint>().First(x => x.TargetPrefab.name.Contains("EZ")).TargetPrefab;
            _ezDoor = door;
            return door;
        }
    }

    public static Dictionary<LockerType, Locker> Lockers
    {
        get
        {
            if (LockerStructures.Count != 0)
                return LockerStructures;

            if (TryGetPrefabOfType("RegularMedkitStructure", out Locker med))
                LockerStructures.Add(LockerType.RegularMedkit, med);
            if (TryGetPrefabOfType("AdrenalineMedkitStructure", out Locker adrenaline))
                LockerStructures.Add(LockerType.AdrenalinMedKit, adrenaline);
            if (TryGetPrefabOfType("LargeGunLockerStructure", out Locker largeGun))
                LockerStructures.Add(LockerType.LargeGun, largeGun);
            if (TryGetPrefabOfType("RifleRackStructure", out Locker rifleRack))
                LockerStructures.Add(LockerType.RifleRack, rifleRack);
            if (TryGetPrefabOfType("MiscLocker", out Locker miscLocker))
                LockerStructures.Add(LockerType.Misc, miscLocker);
            if (TryGetPrefabOfType("Scp018PedestalStructure Variant", out Locker scp018))
                LockerStructures.Add(LockerType.Scp018Pedestal, scp018);
            if (TryGetPrefabOfType("Scp207PedestalStructure Variant", out Locker scp207))
                LockerStructures.Add(LockerType.Scp207Pedestal, scp207);
            if (TryGetPrefabOfType("Scp244PedestalStructure Variant", out Locker scp244))
                LockerStructures.Add(LockerType.Scp244Pedestal, scp244);
            if (TryGetPrefabOfType("Scp268PedestalStructure Variant", out Locker scp268))
                LockerStructures.Add(LockerType.Scp268Pedestal, scp268);
            if (TryGetPrefabOfType("Scp500PedestalStructure Variant", out Locker scp500))
                LockerStructures.Add(LockerType.Scp500Pedestal, scp500);
            if (TryGetPrefabOfType("Scp1853PedestalStructure Variant", out Locker scp1853))
                LockerStructures.Add(LockerType.Scp1853Pedestal, scp1853);
            if (TryGetPrefabOfType("Scp2176PedestalStructure Variant", out Locker scp2176))
                LockerStructures.Add(LockerType.Scp2176Pedestal, scp2176);
            if (TryGetPrefabOfType("Scp1576PedestalStructure Variant", out Locker scp1576))
                LockerStructures.Add(LockerType.Scp1576Pedestal, scp1576);

            return LockerStructures;
        }
    }

    public static bool TryGetPrefabOfType<T>(out T p)
    {
        foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
        {
            if (!prefab.Value.TryGetComponent(out p))
                continue;
            
            return true;
        }

        p = default;
        return false;
    }
    
    public static bool TryGetPrefabOfType<T>(string name, out T p)
    {
        foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
        {
            if (prefab.Value.name != name || !prefab.Value.TryGetComponent(out p))
                continue;
            
            return true;
        }

        p = default;
        return false;
    }
}