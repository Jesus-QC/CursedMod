// -----------------------------------------------------------------------
// <copyright file="CursedSpectatorRole.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.Spectating;
using PlayerRoles.Voice;
using RelativePositioning;

namespace CursedMod.Features.Wrappers.Player.Roles;

public class CursedSpectatorRole : CursedRole
{
    internal CursedSpectatorRole(SpectatorRole roleBase)
        : base(roleBase)
    {
        SpectatorRoleBase = roleBase;
    }
    
    public SpectatorRole SpectatorRoleBase { get; }
    
    public VoiceModuleBase VoiceModule
    {
        get => SpectatorRoleBase.VoiceModule;
        set => SpectatorRoleBase.VoiceModule = value;
    }
    
    public bool ReadyToRespawn => SpectatorRoleBase.ReadyToRespawn;

    public RelativePosition DeathPosition => SpectatorRoleBase.DeathPosition;
    
    public uint SpectatedPlayerNetId => SpectatorRoleBase.SyncedSpectatedNetId;
    
    public CursedPlayer SpectatedPlayer => CursedPlayer.Get(SpectatedPlayerNetId);
}