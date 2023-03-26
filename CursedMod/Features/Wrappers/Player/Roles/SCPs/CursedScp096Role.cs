// -----------------------------------------------------------------------
// <copyright file="CursedScp096Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Subroutines;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp096Role : CursedFpcRole
{
    internal CursedScp096Role(Scp096Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
    }

    public Scp096Role ScpRoleBase { get; }
    
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

    public Scp096StateController StateController
    {
        get => ScpRoleBase.StateController;
        set => ScpRoleBase.StateController = value;
    }

    public void SetState(Scp096RageState state) => StateController.SetRageState(state);

    public void Enrage() => StateController.SetRageState(Scp096RageState.Enraged);

    public void EscapeEnrage() => SetState(Scp096RageState.Distressed);

    public void SetAbilityState(Scp096AbilityState abilityState) => StateController.SetAbilityState(abilityState);
}