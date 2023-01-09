using AdminToys;
using Footprinting;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.AdminToys;

public class CursedAdminToy
{
    public AdminToyBase AdminToyBase { get; }
    public Transform Transform { get; }
    public GameObject GameObject { get; }
    
    public Vector3 Position
    {
        get => Transform.position;
        set => Transform.position = value;
    }
    
    public Vector3 Rotation
    {
        get => Transform.eulerAngles;
        set => Transform.eulerAngles = value;
    }
    
    public Vector3 Scale
    {
        get => Transform.localScale;
        set
        {
            Transform.localScale = value;
            AdminToyBase.NetworkScale = Transform.localScale;
        }
    }

    public Footprint SpawnerFootprint
    {
        get => AdminToyBase.SpawnerFootprint;
        set => AdminToyBase.SpawnerFootprint = value;
    }

    public byte MovementSmoothing
    {
        get => AdminToyBase.MovementSmoothing;
        set => AdminToyBase.NetworkMovementSmoothing = value;
    }

    internal CursedAdminToy(AdminToyBase toyBase)
    {
        AdminToyBase = toyBase;
        Transform = toyBase.transform;
        GameObject = toyBase.gameObject;
    }

    public GameObject Spawn(Vector3? position = null, Vector3? rotation = null, Vector3? scale = null)
    {
        if(position.HasValue)
            Position = position.Value;

        if (rotation.HasValue)
            Rotation = rotation.Value;

        if (scale.HasValue)
            Scale = scale.Value;
        
        NetworkServer.Spawn(GameObject);
        return GameObject;
    }
    
    public GameObject UnSpawn()
    {
        NetworkServer.UnSpawn(GameObject);
        return GameObject;
    }
}