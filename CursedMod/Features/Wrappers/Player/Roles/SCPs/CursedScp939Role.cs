// -----------------------------------------------------------------------
// <copyright file="CursedScp939Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.PlayableScps.Subroutines;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp939Role : CursedFpcRole
{
    internal CursedScp939Role(Scp939Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
    }

    public Scp939Role ScpRoleBase { get; }
    
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