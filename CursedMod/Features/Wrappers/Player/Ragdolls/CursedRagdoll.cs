using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Ragdolls;

public class CursedRagdoll
{
    public BasicRagdoll RagdollBase { get; }
    public Transform Transform { get; }
    public GameObject GameObject { get; }

    public bool AutoCleanUp
    {
        get => !RagdollBase._cleanedUp;
        set => RagdollBase._cleanedUp = !value;
    }

    public RagdollData Data
    {
        get => RagdollBase.Info;
        set => RagdollBase.NetworkInfo = value;
    }

    public void CleanUp() => RagdollBase.OnCleanup();

    public void Destroy() => NetworkServer.Destroy(GameObject);
    
    
    public CursedRagdoll(BasicRagdoll ragdoll)
    {
        RagdollBase = ragdoll;
        Transform = ragdoll.transform;
        GameObject = ragdoll.gameObject;
    }
}