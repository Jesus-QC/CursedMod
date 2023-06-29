// -----------------------------------------------------------------------
// <copyright file="PlayerDiedEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerStatsSystem;

namespace CursedMod.Events.Arguments.Player;

public class PlayerDiedEventArgs : EventArgs, ICursedPlayerEvent
{
    public PlayerDiedEventArgs(PlayerStats stats, DamageHandlerBase damageHandlerBase)
    {
        Player = CursedPlayer.Get(stats._hub);
        DamageHandlerBase = damageHandlerBase;
    }
    
    public CursedPlayer Player { get; }
    
    public DamageHandlerBase DamageHandlerBase { get; }
}