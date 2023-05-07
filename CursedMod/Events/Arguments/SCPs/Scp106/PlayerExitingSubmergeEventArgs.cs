// -----------------------------------------------------------------------
// <copyright file="PlayerExitingSubmergeEventArgs.cs" company="CursedMod">
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

public class PlayerExitingSubmergeEventArgs : EventArgs, ICursedPlayerEvent
{
    public PlayerExitingSubmergeEventArgs(Scp106HuntersAtlasAbility atlasAbility)
    {
        Player = CursedPlayer.Get(atlasAbility.Owner);
        Room = CursedRoom.Get(atlasAbility._syncRoom);
        Position = atlasAbility._syncPos;
    }
    
    public CursedPlayer Player { get; }
    
    public CursedRoom Room { get; }
    
    public Vector3 Position { get; }
}
