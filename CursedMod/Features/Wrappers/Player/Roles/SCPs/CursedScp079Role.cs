// -----------------------------------------------------------------------
// <copyright file="CursedScp079Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using CursedMod.Features.Wrappers.Facility.Rooms;
using MapGeneration;
using Mirror;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Spectating;
using PlayerRoles.Voice;
using RelativePositioning;
using UnityEngine;
using Utils.NonAllocLINQ;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp079Role : CursedRole
{
    internal CursedScp079Role(Scp079Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;

        if (SubroutineModule.TryGetSubroutine(out Scp079BlackoutRoomAbility roomAbility))
            BlackoutRoomAbility = roomAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp079BlackoutZoneAbility zoneAbility))
            BlackoutZoneAbility = zoneAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp079TeslaAbility teslaAbility))
            TeslaAbility = teslaAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp079TierManager tierManager))
            TierManager = tierManager;
        if (SubroutineModule.TryGetSubroutine(out Scp079PingAbility pingAbility))
            PingAbility = pingAbility;
    }

    public Scp079Role ScpRoleBase { get; }
    
    public Scp079BlackoutRoomAbility BlackoutRoomAbility { get; }
    
    public Scp079BlackoutZoneAbility BlackoutZoneAbility { get; }
    
    public Scp079TierManager TierManager { get; }
    
    public Scp079TeslaAbility TeslaAbility { get; }
    
    public Scp079PingAbility PingAbility { get; }

    public SubroutineManagerModule SubroutineModule
    {
        get => ScpRoleBase.SubroutineModule;
        set => ScpRoleBase.SubroutineModule = value;
    }

    public VoiceModuleBase VoiceModule
    {
        get => ScpRoleBase.VoiceModule;
        set => ScpRoleBase.VoiceModule = value;
    }

    public SpectatableModuleBase SpectatorModule
    {
        get => ScpRoleBase.SpectatorModule;
        set => ScpRoleBase.SpectatorModule = value;
    }

    public Vector3 CameraPosition => ScpRoleBase.CameraPosition;
    
    public float VerticalRotation => ScpRoleBase.VerticalRotation;
    
    public float HorizontalRotation => ScpRoleBase.HorizontalRotation;

    public float RollRotation => ScpRoleBase.RollRotation;

    public Cursed079Camera CurrentCamera => new (ScpRoleBase.CurrentCamera);

    public float AmbientLight => ScpRoleBase.AmbientLight;

    public bool InsufficientLight => ScpRoleBase.InsufficientLight;

    public bool IsAfk => ScpRoleBase.IsAFK;

    public Scp079AuxManager AuxManager => BlackoutRoomAbility.AuxManager;

    public int TotalExp
    {
        get => TierManager.TotalExp;
        set => TierManager.TotalExp = value;
    }

    public int RelativeExp => TierManager.RelativeExp;

    public int AccessTierLevel
    {
        get => TierManager.AccessTierLevel;
        set => TierManager.AccessTierIndex = value + 1;
    }
    
    public float CurrentAux
    {
        get => AuxManager.CurrentAux;
        set => AuxManager.CurrentAux = value;
    }

    public void BlackoutRoom()
    {
        if (BlackoutRoomAbility is null)
            return;
        
        BlackoutRoomAbility.AuxManager.CurrentAux -= BlackoutRoomAbility._cost;
        BlackoutRoomAbility.RewardManager.MarkRoom(BlackoutRoomAbility._roomController.Room);
        BlackoutRoomAbility._blackoutCooldowns[BlackoutRoomAbility._roomController.netId] = NetworkTime.time + BlackoutRoomAbility._cooldown;
        BlackoutRoomAbility._roomController.ServerFlickerLights(BlackoutRoomAbility._blackoutDuration);
        BlackoutRoomAbility._successfulController = BlackoutRoomAbility._roomController;
        BlackoutRoomAbility.ServerSendRpc(true);
    }

    public void BlackoutZone()
    {
        if (BlackoutZoneAbility is null)
            return;

        foreach (FlickerableLightController lightController in FlickerableLightController.Instances)
        {
            if (lightController.Room.Zone == BlackoutZoneAbility._syncZone)
            {
                lightController.ServerFlickerLights(BlackoutZoneAbility._duration);
            }
        }
        
        BlackoutZoneAbility._cooldownTimer.Trigger(BlackoutZoneAbility._cooldown);
        BlackoutZoneAbility.AuxManager.CurrentAux -= BlackoutZoneAbility._cost;
        BlackoutZoneAbility.ServerSendRpc(true);
    }

    public void ToggleTesla()
    {
        Scp079Camera cam = TeslaAbility.CurrentCamSync.CurrentCamera;
        TeslaAbility.RewardManager.MarkRoom(cam.Room);
        
        if (!TeslaGateController.Singleton.TeslaGates.TryGetFirst(x => RoomIdUtils.IsTheSameRoom(cam.Position, x.transform.position), out TeslaGate teslaGate))
            return;
        
        TeslaAbility.AuxManager.CurrentAux -= TeslaAbility._cost;
        teslaGate.RpcInstantBurst();
        TeslaAbility._nextUseTime = NetworkTime.time + TeslaAbility._cooldown;
        TeslaAbility.ServerSendRpc(false);
    }

    public void GrantExperience(int amount, Scp079HudTranslation reason) => TierManager.ServerGrantExperience(amount, reason);

    public void SendPing(RelativePosition position, Vector3 normal, List<ReferenceHub> players = null)
    {
        PingAbility._syncPos = position;
        PingAbility._syncNormal = normal;
        
        if (players is null)
            PingAbility.ServerSendRpc(x => PingAbility.ServerCheckReceiver(x, PingAbility._syncPos.Position, (int)PingAbility._syncProcessorIndex));
        else
            PingAbility.ServerSendRpc(players.Contains);
        
        PingAbility.AuxManager.CurrentAux -= PingAbility._cost;
        PingAbility._rateLimiter.RegisterInput();
    }
}