// -----------------------------------------------------------------------
// <copyright file="Scp079UsingBlackoutRoomAbilityEventArgs.cs" company="CursedMod">
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

public class Scp079UsingBlackoutRoomAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079UsingBlackoutRoomAbilityEventArgs(Scp079BlackoutRoomAbility blackoutRoomAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(blackoutRoomAbility.Owner);
        Room = CursedRoom.Get(blackoutRoomAbility._roomController.Room);
        Duration = blackoutRoomAbility._blackoutDuration;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedRoom Room { get; set; }
    
    public float Duration { get; set; }
}