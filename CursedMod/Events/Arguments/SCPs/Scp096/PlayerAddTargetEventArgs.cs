// -----------------------------------------------------------------------
// <copyright file="PlayerAddTargetEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class PlayerAddTargetEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerAddTargetEventArgs(Scp096TargetsTracker targetsTracker, CursedPlayer target, bool isForLooking)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(targetsTracker.Owner);
        Target = target;
        IsForLooking = isForLooking;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public bool IsForLooking { get; set; }
    
    public CursedPlayer Target { get; }
}