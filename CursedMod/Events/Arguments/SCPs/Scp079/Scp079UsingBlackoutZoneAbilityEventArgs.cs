// -----------------------------------------------------------------------
// <copyright file="Scp079UsingBlackoutZoneAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079UsingBlackoutZoneAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079UsingBlackoutZoneAbilityEventArgs(Scp079BlackoutZoneAbility blackoutZoneAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(blackoutZoneAbility.Owner);
        Zone = blackoutZoneAbility._syncZone;
        Duration = blackoutZoneAbility._duration;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public FacilityZone Zone { get; set; }

    public float Duration { get; set; }
}