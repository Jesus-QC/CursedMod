// -----------------------------------------------------------------------
// <copyright file="CursedScp106Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CustomPlayerEffects;
using MapGeneration;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp106;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp106Role : CursedFpcRole
{
    internal CursedScp106Role(Scp106Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
        
        if (SubroutineModule.TryGetSubroutine(out Scp106StalkAbility stalkAbility))
            StalkAbility = stalkAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp106HuntersAtlasAbility huntersAtlasAbility))
            HuntersAtlasAbility = huntersAtlasAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp106Attack attack))
            Attack = attack;
    }

    public Scp106Role ScpRoleBase { get; }
    
    public Scp106StalkAbility StalkAbility { get; }
    
    public Scp106HuntersAtlasAbility HuntersAtlasAbility { get; }

    public Scp106Attack Attack { get; }
    
    public Scp106Vigor Vigor => HuntersAtlasAbility.Vigor;

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

    public bool IsStalkActive
    {
        get => StalkAbility.IsActive;
        set => StalkAbility.IsActive = value;
    }
    
    public float VigorAmount
    {
        get => Vigor.VigorAmount;
        set => Vigor.VigorAmount = value;
    }
    
    public void UseHunterAtlasTeleport(Vector3 position, Vector3 offset)
    {
        if (HuntersAtlasAbility.ScpRole.Sinkhole.NormalizedState > 0f)
            return;
        if (!HuntersAtlasAbility.ScpRole.Sinkhole.Cooldown.IsReady)
            return;
        
        HuntersAtlasAbility._syncRoom = RoomIdUtils.RoomAtPosition(position);
        HuntersAtlasAbility._syncPos = (offset / 50f) + position;
        
        if (HuntersAtlasAbility._syncRoom == null)
            return;
        
        Vector3 position2 = HuntersAtlasAbility.ScpRole.FpcModule.Position;
        if (Mathf.Abs(position2.y - HuntersAtlasAbility._syncPos.y) > 400f)
            return;
        
        float num = (position2 - HuntersAtlasAbility._syncPos).MagnitudeIgnoreY() * 0.019f;
        if (num > HuntersAtlasAbility.Vigor.VigorAmount)
            return;
        
        HuntersAtlasAbility._estimatedCost = num;
        HuntersAtlasAbility.SetSubmerged(true);
    }

    public void SendTargetToPocket(CursedPlayer target)
    {
        Attack._targetHub = target.ReferenceHub;
        DamageHandlerBase handler = new ScpDamageHandler(Attack.Owner, Attack._damage, DeathTranslations.PocketDecay);
        
        if (!Attack._targetHub.playerStats.DealDamage(handler))
            return;
        
        Attack.SendCooldown(Attack._hitCooldown);
        Attack.Vigor.VigorAmount += 0.3f;
        Attack.ReduceSinkholeCooldown();
        Hitmarker.SendHitmarker(Attack.Owner, 1f);
        
        target.EnableEffect<Traumatized>(180f);
        target.EnableEffect<Corroding>();
    }
}