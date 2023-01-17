using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Ragdolls;

public class CursedRagDoll
{
    public BasicRagdoll RagDollBase { get; }
    public Transform Transform { get; }
    public GameObject GameObject { get; }

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
    
    
    public CursedRagDoll(BasicRagdoll ragDoll)
    {
        RagDollBase = ragDoll;
        Transform = ragDoll.transform;
        GameObject = ragDoll.gameObject;
    }
}