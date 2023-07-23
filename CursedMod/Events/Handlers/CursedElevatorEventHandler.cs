// -----------------------------------------------------------------------
// <copyright file="CursedElevatorEventHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Elevators;

namespace CursedMod.Events.Handlers;

public static class CursedElevatorEventHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerInteractingElevatorEventArgs> PlayerInteractingElevator;
    
    public static event CursedEventManager.CursedEventHandler<ElevatorMovingEventArgs> ElevatorMoving; 

    internal static void OnPlayerInteractingElevator(PlayerInteractingElevatorEventArgs args)
    {
        PlayerInteractingElevator.InvokeEvent(args);
    }
    
    internal static void OnElevatorMoving(ElevatorMovingEventArgs args)
    {
        ElevatorMoving.InvokeEvent(args);
    }
}