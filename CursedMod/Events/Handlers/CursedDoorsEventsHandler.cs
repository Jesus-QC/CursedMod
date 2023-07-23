// -----------------------------------------------------------------------
// <copyright file="CursedDoorsEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Doors;

namespace CursedMod.Events.Handlers;

public static class CursedDoorsEventsHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerInteractingDoorEventArgs> PlayerInteractingDoor;

    internal static void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs args)
    {
       PlayerInteractingDoor.InvokeEvent(args); 
    }
}