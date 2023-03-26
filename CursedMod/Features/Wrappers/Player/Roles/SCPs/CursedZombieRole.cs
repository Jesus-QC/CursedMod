// -----------------------------------------------------------------------
// <copyright file="CursedZombieRole.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player.Ragdolls;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerStatsSystem;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedZombieRole : CursedFpcRole
{
    internal CursedZombieRole(ZombieRole roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;

        if (SubroutineModule.TryGetSubroutine(out ZombieBloodlustAbility bloodlustAbility))
            BloodlustAbility = bloodlustAbility;
        if (SubroutineModule.TryGetSubroutine(out ZombieConsumeAbility consumeAbility))
            ConsumeAbility = consumeAbility;
    }

    public ZombieRole ScpRoleBase { get; }
    
    public ZombieBloodlustAbility BloodlustAbility { get; }
    
    public ZombieConsumeAbility ConsumeAbility { get; }

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

    public float RollRotation => ScpRoleBase.RollRotation;

    public void ConsumeRagdoll(CursedRagdoll ragdoll)
    {
        ZombieConsumeAbility.ConsumedRagdolls.Add(ragdoll.Base);
        ConsumeAbility.Owner.playerStats.GetModule<HealthStat>().ServerHeal(100f);
    }
}