using System;
using System.Collections.Generic;
using System.Linq;
using AdminToys;
using CursedMod.Features.Enums;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers;

public static class CursedPrefabManager
{
    private static PrimitiveObjectToy _primitiveObjectToy;
    private static LightSourceToy _lightSourceToy;
    private static ShootingTarget _sportShootingTarget;
    private static ShootingTarget _dBoyShootingTarget;
    private static ShootingTarget _binaryShootingTarget;
    private static DoorVariant _lczDoor;
    private static DoorVariant _hczDoor;
    private static DoorVariant _ezDoor;
    private static Dictionary<LockerType, GameObject> _lockers;

    public static PrimitiveObjectToy PrimitiveObject
    {
        get
        {
            if (_primitiveObjectToy is not null)
                return _primitiveObjectToy;
 
            foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
            {
                if (!prefab.Value.TryGetComponent(out PrimitiveObjectToy toy))
                    continue;
                
                _primitiveObjectToy = toy;
                break;
            }
 
            return _primitiveObjectToy;
        }
    }
    
    public static LightSourceToy LightSource
    {
        get
        {
            if (_lightSourceToy is not null)
                return _lightSourceToy;
 
            foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
            {
                if (!prefab.Value.TryGetComponent(out LightSourceToy toy))
                    continue;
                
                _lightSourceToy = toy;
                break;
            }
 
            return _lightSourceToy;
        }
    }
    
    public static ShootingTarget SportShootingTarget
    {
        get
        {
            if (_sportShootingTarget is not null)
                return _sportShootingTarget;
 
            foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
            {
                if (prefab.Value.name is not "sportTargetPrefab" || !prefab.Value.TryGetComponent(out ShootingTarget toy))
                    continue;
                
                _sportShootingTarget = toy;
                break;
            }
 
            return _sportShootingTarget;
        }
    }
    
    public static ShootingTarget DBoyShootingTarget
    {
        get
        {
            if (_dBoyShootingTarget is not null)
                return _dBoyShootingTarget;
 
            foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
            {
                if (prefab.Value.name is not "dboyTargetPrefab" || !prefab.Value.TryGetComponent(out ShootingTarget toy))
                    continue;
                
                _dBoyShootingTarget = toy;
                break;
            }
 
            return _dBoyShootingTarget;
        }
    }
    
    public static ShootingTarget BinaryShootingTarget
    {
        get
        {
            if (_binaryShootingTarget is not null)
                return _binaryShootingTarget;
 
            foreach (KeyValuePair<Guid, GameObject> prefab in NetworkClient.prefabs)
            {
                if (prefab.Value.name is not "binaryTargetPrefab" || !prefab.Value.TryGetComponent(out ShootingTarget toy))
                    continue;
                
                _binaryShootingTarget = toy;
                break;
            }
 
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

    public static Dictionary<LockerType, GameObject> Lockers
    {
        get
        {
            if (_lockers is not null)
                return _lockers;

            _lockers.Add(LockerType.RegularMedkit, NetworkClient.prefabs.Values.First(x => x.name == "RegularMedkitStructure"));
            _lockers.Add(LockerType.AdrenalinMedKit, NetworkClient.prefabs.Values.First(x => x.name == "AdrenalineMedkitStructure"));
            _lockers.Add(LockerType.LargeGun, NetworkClient.prefabs.Values.First(x => x.name == "LargeGunLockerStructure"));
            _lockers.Add(LockerType.RifleRack, NetworkClient.prefabs.Values.First(x => x.name == "RifleRackStructure"));
            _lockers.Add(LockerType.Misc, NetworkClient.prefabs.Values.First(x => x.name == "MiscLocker"));
            _lockers.Add(LockerType.Scp018Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp018PedestalStructure Variant"));
            _lockers.Add(LockerType.Scp207Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp207PedestalStructure"));
            _lockers.Add(LockerType.Scp244Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp244PedestalStructure"));
            _lockers.Add(LockerType.Scp268Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp268PedestalStructure"));
            _lockers.Add(LockerType.Scp500Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp500PedestalStructure"));
            _lockers.Add(LockerType.Scp1853Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp1853PedestalStructure"));
            _lockers.Add(LockerType.Scp2176Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp2176PedestalStructure"));
            _lockers.Add(LockerType.Scp1576Pedestal, NetworkClient.prefabs.Values.First(x => x.name == "Scp1576PedestalStructure"));

            return _lockers;
        }
    }
}