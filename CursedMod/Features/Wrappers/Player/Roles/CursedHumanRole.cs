// -----------------------------------------------------------------------
// <copyright file="CursedHumanRole.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles;
using Respawning;

namespace CursedMod.Features.Wrappers.Player.Roles;

public class CursedHumanRole : CursedFpcRole
{
    internal CursedHumanRole(HumanRole roleBase)
        : base(roleBase)
    {
        HumanRoleBase = roleBase;
    }
    
    public HumanRole HumanRoleBase { get; }

    public SpawnableTeamType AssignedSpawnableTeam
    {
        get => HumanRoleBase.AssignedSpawnableTeam;
        set => HumanRoleBase.AssignedSpawnableTeam = value;
    }

    public byte UnitNameId
    {
        get => HumanRoleBase.UnitNameId;
        set => HumanRoleBase.UnitNameId = value;
    }

    public bool UsesUnitName => HumanRoleBase.UsesUnitNames;

    public string UnitName => HumanRoleBase.UnitName;
}