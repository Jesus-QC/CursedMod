// -----------------------------------------------------------------------
// <copyright file="Scp096AddingTargetEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class Scp096AddingTargetEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp096AddingTargetEventArgs(Scp096TargetsTracker targetsTracker, ReferenceHub target, bool isForLooking)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(targetsTracker.Owner);
        Target = CursedPlayer.Get(target);
        IsForLooking = isForLooking;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public bool IsForLooking { get; set; }
    
    public CursedPlayer Target { get; }
}