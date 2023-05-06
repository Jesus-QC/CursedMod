// -----------------------------------------------------------------------
// <copyright file="PlayerUseHunterAtlasEventArgs.cs" company="CursedMod">
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

public class PlayerUseHunterAtlasEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerUseHunterAtlasEventArgs(Scp106HuntersAtlasAbility huntersAtlasAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(huntersAtlasAbility.Owner);
        Position = huntersAtlasAbility._syncPos;
        Room = CursedRoom.Get(huntersAtlasAbility._syncRoom);
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public Vector3 Position { get; }
    
    public CursedRoom Room { get; }
}