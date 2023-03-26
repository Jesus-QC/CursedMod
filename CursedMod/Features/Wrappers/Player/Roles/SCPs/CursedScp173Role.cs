// -----------------------------------------------------------------------
// <copyright file="CursedScp173Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerStatsSystem;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp173Role : CursedFpcRole
{
    internal CursedScp173Role(Scp173Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
    }

    public Scp173Role ScpRoleBase { get; }
    
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

    public bool DamagedEventActive
    {
        get => ScpRoleBase.DamagedEventActive;
        set => ScpRoleBase.DamagedEventActive = value;
    }

    public ScpDamageHandler DamageHandler => ScpRoleBase.DamageHandler;
}