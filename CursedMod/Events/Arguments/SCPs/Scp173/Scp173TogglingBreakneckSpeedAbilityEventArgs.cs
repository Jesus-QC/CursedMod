// -----------------------------------------------------------------------
// <copyright file="Scp173TogglingBreakneckSpeedAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp173;

namespace CursedMod.Events.Arguments.SCPs.Scp173;

public class Scp173TogglingBreakneckSpeedAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp173TogglingBreakneckSpeedAbilityEventArgs(Scp173BreakneckSpeedsAbility breakneckSpeedsAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(breakneckSpeedsAbility.Owner);
        NewState = !(breakneckSpeedsAbility.IsActive && breakneckSpeedsAbility.Elapsed >= 1f);
    }

    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public bool NewState { get; }
}