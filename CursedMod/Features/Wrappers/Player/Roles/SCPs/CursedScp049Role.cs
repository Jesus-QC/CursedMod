// -----------------------------------------------------------------------
// <copyright file="CursedScp049Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Subroutines;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp049Role : CursedFpcRole
{
    internal CursedScp049Role(Scp049Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
    }

    public Scp049Role ScpRoleBase { get; }

    public HumeShieldModuleBase HumeShieldModule
    {
        get => ScpRoleBase.HumeShieldModule;
        set => ScpRoleBase.HumeShieldModule = value;
    }

    public SubroutineManagerModule SubroutineModule
    {
        get => ScpRoleBase.SubroutineModule;
        set => ScpRoleBase.SubroutineModule = value;
    }
}