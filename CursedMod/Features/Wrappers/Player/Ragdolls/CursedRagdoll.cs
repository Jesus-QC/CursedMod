using System.Collections.Generic;
using System.Linq;
using Mirror;
using PlayerRoles;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Ragdolls;

public class CursedRagdoll
{
    internal static List<CursedRagdoll> BasicRagdolls { get; } = new List<CursedRagdoll>();
    public static IReadOnlyCollection<CursedRagdoll> List => BasicRagdolls.AsReadOnly();

    public BasicRagdoll Base { get; }

    internal CursedRagdoll(BasicRagdoll ragdoll)
    {
        Base = ragdoll;
    }

    public bool AutoCleanUp
    {
        get => !Base._cleanedUp;
        set => Base._cleanedUp = !value;
    }

    public RoleTypeId Role => Base.Info.RoleType;

    public CursedPlayer Owner => CursedPlayer.Get(Base.Info.OwnerHub);

    public string Nickname => Base.NetworkInfo.Nickname;

    public Vector3 Position => Base.gameObject.transform.position;

    public RagdollData Data => Base.Info;

    public void CleanUp() => Base.OnCleanup();

    public void Destroy() => NetworkServer.Destroy(Base.gameObject);

    public static CursedRagdoll Get(BasicRagdoll basicRagdoll) => List.FirstOrDefault(ragdoll => ragdoll.Base == basicRagdoll);

    public static IEnumerable<CursedRagdoll> Get(CursedPlayer player) => List.Where(ragdoll => ragdoll.Owner == player);
}