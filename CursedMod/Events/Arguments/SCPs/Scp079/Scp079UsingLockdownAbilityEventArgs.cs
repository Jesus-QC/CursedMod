﻿// -----------------------------------------------------------------------
// <copyright file="Scp079UsingLockdownAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Rooms;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079UsingLockdownAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079UsingLockdownAbilityEventArgs(Scp079LockdownRoomAbility lockdownRoomAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(lockdownRoomAbility.Owner);
        Room = CursedRoom.Get(lockdownRoomAbility.CurrentCamSync.CurrentCamera.Room);
        Duration = lockdownRoomAbility._lockdownDuration;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedRoom Room { get; }
    
    public float Duration { get; set; }
}