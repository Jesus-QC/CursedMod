// -----------------------------------------------------------------------
// <copyright file="PlayerReceivingDamageEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerStatsSystem;

namespace CursedMod.Events.Arguments.Player;

public class PlayerReceivingDamageEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerReceivingDamageEventArgs(PlayerStats playerStats, DamageHandlerBase damageHandlerBase)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(playerStats._hub);
        DamageHandlerBase = damageHandlerBase;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }

    public DamageHandlerBase DamageHandlerBase { get; } // todo: this event has to be recoded
}