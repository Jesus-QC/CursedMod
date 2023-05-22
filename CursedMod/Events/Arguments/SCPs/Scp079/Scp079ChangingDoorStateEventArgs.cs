// -----------------------------------------------------------------------
// <copyright file="Scp079ChangingDoorStateEventArgs.cs" company="CursedMod">
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

public class Scp079ChangingDoorStateEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079ChangingDoorStateEventArgs(Scp079DoorStateChanger doorStateChanger)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(doorStateChanger.Owner);
        Door = CursedDoor.Get(doorStateChanger.LastDoor);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedDoor Door { get; }
}