// -----------------------------------------------------------------------
// <copyright file="PlayerInteractingDoorEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Doors;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects.DoorUtils;

namespace CursedMod.Events.Arguments.Facility.Doors;

public class PlayerInteractingDoorEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerInteractingDoorEventArgs(DoorVariant doorVariant, ReferenceHub ply, bool hasPermissions)
    {
        IsAllowed = true;
        Door = CursedDoor.Get(doorVariant);
        Player = CursedPlayer.Get(ply);
        HasPermissions = hasPermissions;
    }
    
    public bool IsAllowed { get; set; }
 
    public CursedPlayer Player { get; }
    
    public CursedDoor Door { get; }
    
    public bool HasPermissions { get; set; }
}