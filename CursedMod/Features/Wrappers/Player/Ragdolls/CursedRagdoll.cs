// -----------------------------------------------------------------------
// <copyright file="CursedRagdoll.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Mirror;
using PlayerRoles;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Ragdolls;

public class CursedRagdoll
{
    internal static readonly HashSet<CursedRagdoll> Ragdolls = new ();
    
    private CursedRagdoll(BasicRagdoll ragdoll)
    {
        Base = ragdoll;
        Ragdolls.Add(this);
    }

    public static IReadOnlyCollection<CursedRagdoll> Collection => Ragdolls;

    public BasicRagdoll Base { get; }

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

    public static CursedRagdoll Get(BasicRagdoll basicRagdoll)
    {
        foreach (CursedRagdoll ragdoll in Ragdolls)
        {
            if (basicRagdoll == ragdoll.Base)
                return ragdoll;
        }

        return new CursedRagdoll(basicRagdoll);
    }

    public static IEnumerable<CursedRagdoll> Get(CursedPlayer player)
    {
        foreach (CursedRagdoll ragdoll in Ragdolls)
        {
            if (player == ragdoll.Owner)
                yield return ragdoll;
        }
    }
    
    public void CleanUp() => Base.OnCleanup();

    public void Destroy() => NetworkServer.Destroy(Base.gameObject);
}