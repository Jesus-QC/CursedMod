// -----------------------------------------------------------------------
// <copyright file="PlayerPryGateEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Doors;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class PlayerPryGateEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerPryGateEventArgs(Scp096PrygateAbility pryGateAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(pryGateAbility.Owner);
        Gate = CursedDoor.Get(pryGateAbility._syncDoor);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedDoor Gate { get; }
}