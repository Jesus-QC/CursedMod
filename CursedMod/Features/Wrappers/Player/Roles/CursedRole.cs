// -----------------------------------------------------------------------
// <copyright file="CursedRole.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player.Roles.SCPs;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.Scp079;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles;

public class CursedRole
{
    internal CursedRole(PlayerRoleBase roleBase)
    {
        RoleBase = roleBase;
    }

    public PlayerRoleBase RoleBase { get; }

    public RoleTypeId RoleTypeId => RoleBase.RoleTypeId;

    public Team Team => RoleBase.Team;

    public Color RoleColor => RoleBase.RoleColor;

    public string Name => RoleBase.RoleName;

    public float ActiveTime => RoleBase.ActiveTime;

    public RoleChangeReason SpawnReason
    {
        get => RoleBase.ServerSpawnReason;
        set => RoleBase.ServerSpawnReason = value;
    }

    public RoleSpawnFlags SpawnFlags
    {
        get => RoleBase.ServerSpawnFlags;
        set => RoleBase.ServerSpawnFlags = value;
    }

    public static CursedRole Get(PlayerRoleBase roleBase)
    {
        return roleBase switch
        {
            FpcStandardRoleBase fpcRole => CursedFpcRole.Get(fpcRole),
            Scp079Role scp079Role => new CursedScp079Role(scp079Role),
            NoneRole noneRole => new CursedNoneRole(noneRole),
            _ => new CursedRole(roleBase)
        };
    }
}