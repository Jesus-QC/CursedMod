// -----------------------------------------------------------------------
// <copyright file="CursedFpcRole.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player.Roles.SCPs;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Scp106;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.Spectating;
using PlayerRoles.Visibility;
using PlayerRoles.Voice;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles;

public class CursedFpcRole : CursedRole
{
    internal CursedFpcRole(FpcStandardRoleBase roleBase)
        : base(roleBase)
    {
        FpcRoleBase = roleBase;
    }

    public FpcStandardRoleBase FpcRoleBase { get; }

    public Vector3 CameraPosition => FpcRoleBase.CameraPosition;
    
    public float VerticalRotation => FpcRoleBase.VerticalRotation;
    
    public float HorizontalRotation => FpcRoleBase.HorizontalRotation;

    public float MaxHealth => FpcRoleBase.MaxHealth;

    public ISpawnpointHandler SpawnPointHandler => FpcRoleBase.SpawnpointHandler;

    public Color AmbientLight => FpcRoleBase.AmbientLight;

    public bool InsufficientLight => FpcRoleBase.InsufficientLight;

    public FirstPersonMovementModule FpcModule
    {
        get => FpcRoleBase.FpcModule;
        set => FpcRoleBase.FpcModule = value;
    }
    
    public SpectatableModuleBase SpectatorModule
    {
        get => FpcRoleBase.SpectatorModule;
        set => FpcRoleBase.SpectatorModule = value;
    }

    public VoiceModuleBase VoiceModule
    {
        get => FpcRoleBase.VoiceModule;
        set => FpcRoleBase.VoiceModule = value;
    }

    public VisibilityController VisibilityController
    {
        get => FpcRoleBase.VisibilityController;
        set => FpcRoleBase.VisibilityController = value;
    }

    public BasicRagdoll Ragdoll
    {
        get => FpcRoleBase.Ragdoll;
        set => FpcRoleBase.Ragdoll = value;
    }

    public bool IsInDarkness => FpcRoleBase.InDarkness;

    public bool HasFlashlightEnabled => FpcRoleBase.HasFlashlight;

    public bool IsAfk => FpcRoleBase.IsAFK;
    
    public static CursedFpcRole Get(FpcStandardRoleBase roleBase)
    {
        return roleBase switch
        {
            HumanRole humanRole => new CursedHumanRole(humanRole),
            ZombieRole zombieRole => new CursedZombieRole(zombieRole),
            Scp049Role scp049Role => new CursedScp049Role(scp049Role),
            Scp096Role scp096Role => new CursedScp096Role(scp096Role),
            Scp106Role scp106Role => new CursedScp106Role(scp106Role),
            Scp173Role scp173Role => new CursedScp173Role(scp173Role),
            Scp939Role scp939Role => new CursedScp939Role(scp939Role),
            _ => new CursedFpcRole(roleBase)
        };
    }
}