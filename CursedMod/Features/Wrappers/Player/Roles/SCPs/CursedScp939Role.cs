// -----------------------------------------------------------------------
// <copyright file="CursedScp939Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using PlayerRoles.PlayableScps.Subroutines;
using RelativePositioning;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp939Role : CursedFpcRole
{
    internal CursedScp939Role(Scp939Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;
        
        if (SubroutineModule.TryGetSubroutine(out Scp939AmnesticCloudAbility amnesticCloud))
            AmnesticCloudAbility = amnesticCloud;
        if (SubroutineModule.TryGetSubroutine(out Scp939LungeAbility lungeAbility))
            LungeAbility = lungeAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp939FocusAbility focusAbility))
            FocusAbility = focusAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp939ClawAbility clawAbility))
            ClawAbility = clawAbility;
        if (!SubroutineModule.TryGetSubroutine(out EnvironmentalMimicry environmentalMimicry)) 
            return;
        
        EnvironmentalMimicry = environmentalMimicry;
        MimicPointController = environmentalMimicry._mimicPoint;
    }

    public Scp939Role ScpRoleBase { get; }
    
    public Scp939AmnesticCloudAbility AmnesticCloudAbility { get; }
    
    public Scp939LungeAbility LungeAbility { get; }
    
    public Scp939FocusAbility FocusAbility { get; }
    
    public Scp939ClawAbility ClawAbility { get; }
    
    public EnvironmentalMimicry EnvironmentalMimicry { get; }
    
    public MimicPointController MimicPointController { get; }
    
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
    
    public Scp939LungeState LungeState
    {
        get => LungeAbility._state;
        set => LungeAbility.State = value;
    }

    public bool FocusEnabled
    {
        get => FocusAbility._targetState;
        set => FocusAbility.TargetState = value;
    } 
        
    public void CreateAmnesticCloud(float durationSize)
    {
        AmnesticCloudAbility.OnStateEnabled();
        AmnesticCloudAbility.ServerConfirmPlacement(durationSize);
    }

    public void PlayMimicrySound(byte sound, int cooldown = 0)
    {
        EnvironmentalMimicry._syncOption = sound;
        EnvironmentalMimicry.Cooldown.Trigger(cooldown);
        EnvironmentalMimicry.ServerSendRpc(true);
    }

    public void SetMimicPoint(Vector3 position)
    {
        MimicPointController._syncMessage = MimicPointController.RpcStateMsg.PlacedByUser;
        MimicPointController._syncPos = new RelativePosition(position);
        MimicPointController.Active = true;
        MimicPointController.ServerSendRpc(true);
    }

    public void RemoveMimicPoint()
    {
        MimicPointController._syncMessage = MimicPointController.RpcStateMsg.RemovedByUser;
        MimicPointController.Active = false;
        MimicPointController.ServerSendRpc(true);
    }
}