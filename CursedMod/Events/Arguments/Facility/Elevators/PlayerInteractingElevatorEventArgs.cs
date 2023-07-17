// -----------------------------------------------------------------------
// <copyright file="PlayerInteractingElevatorEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Elevators;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects;

namespace CursedMod.Events.Arguments.Facility.Elevators;

public class PlayerInteractingElevatorEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedElevatorEvent
{
    public PlayerInteractingElevatorEventArgs(ReferenceHub player, ElevatorChamber elevatorChamber, int targetLevel)
    {
        Player = CursedPlayer.Get(player);
        ElevatorChamber = CursedElevatorChamber.Get(elevatorChamber);
        TargetLevel = targetLevel;
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }

    public CursedElevatorChamber ElevatorChamber { get; }
    
    public int TargetLevel { get; set; }

    public bool IsAllowed { get; set; }
}