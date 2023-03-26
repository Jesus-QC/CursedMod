// -----------------------------------------------------------------------
// <copyright file="CursedScp106Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp106;
using PlayerRoles.PlayableScps.Subroutines;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp106Role : CursedFpcRole
{
    internal CursedScp106Role(Scp106Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
    }

    public Scp106Role ScpRoleBase { get; }
    
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

    public Scp106SinkholeController SinkholeController => ScpRoleBase.Sinkhole;

    public bool CanActivateIdle => ScpRoleBase.CanActivateIdle;

    public bool CanActivateShock => ScpRoleBase.CanActivateShock;

    public bool IsSubmerged => ScpRoleBase.IsSubmerged;
}