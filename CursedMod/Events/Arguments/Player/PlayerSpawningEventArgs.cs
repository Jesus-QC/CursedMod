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
using UnityEngine;

namespace CursedMod.Events.Arguments.Player;

public class PlayerSpawningEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerSpawningEventArgs(ReferenceHub referenceHub, PlayerRoleBase roleBase, Vector3 position, float rotation)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(referenceHub);
        RoleType = roleBase.RoleTypeId;
        SpawnPosition = position;
        SpawnRotation = rotation;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public RoleTypeId RoleType { get; }
    
    public Vector3 SpawnPosition { get; set; }
    
    public float SpawnRotation { get; set; }
}