// -----------------------------------------------------------------------
// <copyright file="PlayerMoveElevatorEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Rooms;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class PlayerMoveElevatorEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerMoveElevatorEventArgs(Scp079ElevatorStateChanger elevatorStateChanger, ElevatorDoor elevatorDoor, RoomIdentifier roomIdentifier)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(elevatorStateChanger.Owner);
        PowerCost = elevatorStateChanger._cost;
        ElevatorChamber = elevatorDoor.TargetPanel.AssignedChamber;
        Room = CursedRoom.Get(roomIdentifier);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public float PowerCost { get; set; }

    public ElevatorChamber ElevatorChamber { get; }
    
    public CursedRoom Room { get; }
}