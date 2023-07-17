// -----------------------------------------------------------------------
// <copyright file="PlayerEnteringHazardEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Hazards;
using CursedMod.Features.Wrappers.Player;
using Hazards;

namespace CursedMod.Events.Arguments.Facility.Hazards;

public class PlayerEnteringHazardEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedHazardEvent
{
    public PlayerEnteringHazardEventArgs(ReferenceHub player, EnvironmentalHazard hazard)
    {
        Player = CursedPlayer.Get(player);
        Hazard = CursedEnvironmentalHazard.Get(hazard);
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }

    public CursedEnvironmentalHazard Hazard { get; }

    public bool IsAllowed { get; set; }
}