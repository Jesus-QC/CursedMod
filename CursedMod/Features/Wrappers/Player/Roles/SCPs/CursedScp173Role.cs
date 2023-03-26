// -----------------------------------------------------------------------
// <copyright file="CursedScp173Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Hazards;
using Mirror;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerStatsSystem;
using RelativePositioning;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp173Role : CursedFpcRole
{
    internal CursedScp173Role(Scp173Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
        
        if (SubroutineModule.TryGetSubroutine(out Scp173BreakneckSpeedsAbility breakneckSpeedsAbility))
            BreakneckSpeedsAbility = breakneckSpeedsAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp173TantrumAbility tantrumAbility))
            TantrumAbility = tantrumAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp173TeleportAbility teleportAbility))
            TeleportAbility = teleportAbility;
    }

    public Scp173Role ScpRoleBase { get; }
    
    public Scp173BreakneckSpeedsAbility BreakneckSpeedsAbility { get; }
    
    public Scp173TantrumAbility TantrumAbility { get; }
    
    public Scp173TeleportAbility TeleportAbility { get; }
    
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

    public bool BreakNeckSpeeds
    {
        get => BreakneckSpeedsAbility.IsActive;
        set => BreakneckSpeedsAbility.IsActive = value;
    }

    public void CreateTantrum()
    {
        RaycastHit raycastHit;
        if (!Physics.Raycast(TantrumAbility.ScpRole.FpcModule.Position, Vector3.down, out raycastHit, 3f, TantrumAbility._tantrumMask))
        {
            return;
        }
      
        TantrumAbility.Cooldown.Trigger(30f);
        TantrumAbility.ServerSendRpc(true);
        TantrumEnvironmentalHazard tantrumEnvironmentalHazard = Object.Instantiate(TantrumAbility._tantrumPrefab);
        Vector3 targetPos = raycastHit.point + (Vector3.up * 1.25f);
        tantrumEnvironmentalHazard.SynchronizedPosition = new RelativePosition(targetPos);
        
        NetworkServer.Spawn(tantrumEnvironmentalHazard.gameObject);
        
        foreach (TeslaGate teslaGate in TeslaGateController.Singleton.TeslaGates)
        {
            if (teslaGate.PlayerInIdleRange(TantrumAbility.Owner))
            {
                teslaGate.TantrumsToBeDestroyed.Add(tantrumEnvironmentalHazard);
            }
        }
    }

    public void BlinkToPosition(Vector3 position) => TeleportAbility._blinkTimer.ServerBlink(position);
}