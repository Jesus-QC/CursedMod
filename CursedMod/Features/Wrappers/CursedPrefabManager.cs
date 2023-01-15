using System;
using System.Collections.Generic;
using System.Linq;
using AdminToys;
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
}