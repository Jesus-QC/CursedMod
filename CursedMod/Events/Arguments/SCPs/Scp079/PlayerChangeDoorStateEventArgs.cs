// -----------------------------------------------------------------------
// <copyright file="PlayerChangeDoorStateEventArgs.cs" company="CursedMod">
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

public class PlayerChangeDoorStateEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerChangeDoorStateEventArgs(Scp079DoorStateChanger doorStateChanger, DoorVariant variant)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(doorStateChanger.Owner);
        Door = CursedDoor.Get(variant);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedDoor Door { get; }
}