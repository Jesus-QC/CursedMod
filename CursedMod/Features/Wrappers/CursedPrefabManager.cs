using System;
using System.Collections.Generic;
using AdminToys;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers;

public static class CursedPrefabManager
{
    // cached prefabs
    private static PrimitiveObjectToy _primitiveObjectToy;
    private static LightSourceToy _lightSourceToy;
    
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
}