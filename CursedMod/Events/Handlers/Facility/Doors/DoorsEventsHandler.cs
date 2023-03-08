// -----------------------------------------------------------------------
// <copyright file="DoorsEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Doors;

namespace CursedMod.Events.Handlers.Facility.Doors;

public static class DoorsEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerInteractingDoorEventArgs> PlayerInteractingDoor;

    public static void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs args)
    {
       PlayerInteractingDoor.InvokeEvent(args); 
    }
}