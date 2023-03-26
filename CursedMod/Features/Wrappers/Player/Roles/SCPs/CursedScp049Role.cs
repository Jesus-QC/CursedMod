// -----------------------------------------------------------------------
// <copyright file="CursedScp049Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player.Ragdolls;
using CustomPlayerEffects;
using PlayerRoles;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp049Role : CursedFpcRole
{
    internal CursedScp049Role(Scp049Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;

        if (SubroutineModule.TryGetSubroutine(out Scp049AttackAbility attackAbility))
            AttackAbility = attackAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp049SenseAbility senseAbility))
            SenseAbility = senseAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp049CallAbility callAbility))
            CallAbility = callAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp049ResurrectAbility resurrectAbility))
            ResurrectAbility = resurrectAbility;
    }

    public Scp049Role ScpRoleBase { get; }

    public Scp049AttackAbility AttackAbility { get; }
    
    public Scp049CallAbility CallAbility { get; }
    
    public Scp049ResurrectAbility ResurrectAbility { get; }

    public Scp049SenseAbility SenseAbility { get; }

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

    public void Attack(CursedPlayer target)
    {
        AttackAbility._target = target.ReferenceHub;
        
        if (AttackAbility._target == null || !AttackAbility.IsTargetValid(AttackAbility._target))
            return;

        AttackAbility.Cooldown.Trigger(1.5f);
        CardiacArrest effect = AttackAbility._target.playerEffectsController.GetEffect<CardiacArrest>();
        
        if (effect.IsEnabled)
        {
            AttackAbility._target.playerStats.DealDamage(new Scp049DamageHandler(AttackAbility.Owner, -1f, Scp049DamageHandler.AttackType.Instakill));
        }
        else
        {
            effect.SetAttacker(AttackAbility.Owner);
            effect.Intensity = 1;
            effect.ServerChangeDuration(AttackAbility._statusEffectDuration, false);
        }

        SenseAbility.HasTarget = false;
        SenseAbility.Cooldown.Trigger(20f);
        SenseAbility.ServerSendRpc(true);
        
        AttackAbility.ServerSendRpc(true);
        Hitmarker.SendHitmarker(AttackAbility.Owner, 1f);
    }

    public void StartCallAbility(float cooldown = 20f)
    {
        CallAbility.Duration.Trigger(cooldown);
        CallAbility._serverTriggered = true;
        CallAbility.ServerSendRpc(true);
    }

    public void Resurrect(CursedRagdoll ragdoll)
    {
        Resurrect(ragdoll.Owner);
        ragdoll.Destroy();
    }

    public void Resurrect(CursedPlayer player)
    {
        ReferenceHub ownerHub = player.ReferenceHub;
  
        ownerHub.transform.position = ResurrectAbility.ScpRole.FpcModule.Position;
        
        if (SenseAbility.DeadTargets.Contains(ownerHub))
        {
            HumeShieldModuleBase humeShieldModule = ResurrectAbility.ScpRole.HumeShieldModule;
            humeShieldModule.HsCurrent = Mathf.Min(humeShieldModule.HsCurrent + 100f, humeShieldModule.HsMax);
        }
        
        ownerHub.roleManager.ServerSetRole(RoleTypeId.Scp0492, RoleChangeReason.Revived);
    }
}