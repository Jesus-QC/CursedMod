using CursedMod.Features.Wrappers.Server;
using InventorySystem.Items.Pickups;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using System;
using System.CodeDom;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Ragdolls;

public class CursedRagDoll
{
    public BasicRagdoll RagDollBase { get; }
    public Transform Transform { get; }
    public GameObject GameObject { get; }

    public static CursedRagDoll Get(BasicRagdoll ragDoll) => new(ragDoll);

    public static CursedRagDoll Create(RagdollData data, bool spawn = true)
    {
        IRagdollRole role = CursedServer.LocalPlayer.RoleManager.GetRoleBase(data.RoleType) as IRagdollRole;
        GameObject gameObject = role.Ragdoll.gameObject;

        if (gameObject == null)
            throw new InvalidOperationException($"Failed to create ragdoll ({data.RoleType}) because the Game Object is null.");

        var ragdollObj = UnityEngine.Object.Instantiate(gameObject);

        if (!ragdollObj.TryGetComponent(out BasicRagdoll ragdoll))
            throw new InvalidOperationException($"Failed to create ragdoll ({data.RoleType}) because it did not have a BasicRagdoll component.");

        ragdoll.NetworkInfo = data;

        var cursedRagdoll = Get(ragdoll);
        if (spawn)
            cursedRagdoll.Spawn();

        return cursedRagdoll;
    }

    public void Spawn()
        => NetworkServer.Spawn(GameObject);

    public void Unspawn()
        => NetworkServer.UnSpawn(GameObject);

    public Vector3 Position
    {
        get => Transform.position;
        set => RunActionAndRespawn(() => Transform.position = value);
    }

    public Quaternion Rotation
    {
        get => Transform.rotation;
        set => RunActionAndRespawn(() => Transform.rotation = value);
    }

    public Vector3 Scale
    {
        get => Transform.localScale;
        set => RunActionAndRespawn(() => Transform.localScale = value);
    }

    public bool AutoCleanUp
    {
        get => !RagDollBase._cleanedUp;
        set => RagDollBase._cleanedUp = !value;
    }

    public RagdollData Data
    {
        get => RagDollBase.Info;
        set => RagDollBase.NetworkInfo = value;
    }

    public void CleanUp() => RagDollBase.OnCleanup();

    public void Destroy() => NetworkServer.Destroy(GameObject);

    public void RunActionAndRespawn(Action action)
    {
        Unspawn();
        action();
        Spawn();
    }
    
    public CursedRagDoll(BasicRagdoll ragDoll)
    {
        RagDollBase = ragDoll;
        Transform = ragDoll.transform;
        GameObject = ragDoll.gameObject;
    }
}