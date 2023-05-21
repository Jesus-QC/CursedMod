// -----------------------------------------------------------------------
// <copyright file="Scp106ExitingSubmergenceEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Rooms;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp106;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp106;

public class Scp106ExitingSubmergenceEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp106ExitingSubmergenceEventArgs(Scp106HuntersAtlasAbility atlasAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(atlasAbility.Owner);
        Room = CursedRoom.Get(atlasAbility._syncRoom);
        Position = atlasAbility._syncPos;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedRoom Room { get; }
    
    public Vector3 Position { get; }
}