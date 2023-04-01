// -----------------------------------------------------------------------
// <copyright file="PlayerSpawningEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using RelativePositioning;

namespace CursedMod.Events.Arguments.Player;

public class PlayerSpawningEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerSpawningEventArgs(FpcStandardRoleBase fpcStandardRoleBase, RelativePosition relativePosition, ushort rotation)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(fpcStandardRoleBase._lastOwner);
        RoleType = fpcStandardRoleBase.RoleTypeId;
        SpawnPosition = relativePosition;
        SpawnRotation = rotation;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public RoleTypeId RoleType { get; }
    
    public RelativePosition SpawnPosition { get; set; }
    
    public ushort SpawnRotation { get; set; }
}