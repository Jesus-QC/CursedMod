// -----------------------------------------------------------------------
// <copyright file="ElevatorMovingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Elevators;
using Interactables.Interobjects;
using UnityEngine;

namespace CursedMod.Events.Arguments.Facility.Elevators;

public class ElevatorMovingEventArgs : EventArgs, ICursedElevatorEvent
{
    public ElevatorMovingEventArgs(ElevatorChamber elevatorChamber, Bounds worldBounds, Vector3 deltaPos, Quaternion deltaRot)
    {
        ElevatorChamber = CursedElevatorChamber.Get(elevatorChamber);
        WorldSpaceBounds = worldBounds;
        DeltaPosition = deltaPos;
        DeltaRotation = deltaRot;
    }

    public CursedElevatorChamber ElevatorChamber { get; }
    
    public Bounds WorldSpaceBounds { get; }
    
    public Vector3 DeltaPosition { get; }
    
    public Quaternion DeltaRotation { get; }
}