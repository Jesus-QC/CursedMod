// -----------------------------------------------------------------------
// <copyright file="Scp079MovingElevatorEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Elevators;
using CursedMod.Features.Wrappers.Facility.Rooms;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079MovingElevatorEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079MovingElevatorEventArgs(Scp079ElevatorStateChanger elevatorStateChanger, ElevatorDoor elevatorDoor)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(elevatorStateChanger.Owner);
        ElevatorChamber = CursedElevatorChamber.Get(elevatorDoor.TargetPanel.AssignedChamber);
        Room = CursedRoom.Get(elevatorStateChanger.CurrentCamSync.CurrentCamera.Room);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public CursedElevatorChamber ElevatorChamber { get; }
    
    public CursedRoom Room { get; }
}