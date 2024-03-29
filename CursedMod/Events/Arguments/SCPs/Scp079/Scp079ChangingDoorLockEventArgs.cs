﻿// -----------------------------------------------------------------------
// <copyright file="Scp079ChangingDoorLockEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Doors;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects.DoorUtils;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079ChangingDoorLockEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079ChangingDoorLockEventArgs(Scp079DoorLockChanger doorLockChanger, DoorVariant door, bool newLockState, bool skipChecks)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(doorLockChanger.Owner);
        Door = CursedDoor.Get(door);
        NewLockState = newLockState;
        SkipChecks = skipChecks;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedDoor Door { get; set; }
    
    public bool NewLockState { get; set; }
    
    public bool SkipChecks { get; set; }
}