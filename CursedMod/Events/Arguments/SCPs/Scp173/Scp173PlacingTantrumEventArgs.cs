// -----------------------------------------------------------------------
// <copyright file="Scp173PlacingTantrumEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp173;

namespace CursedMod.Events.Arguments.SCPs.Scp173;

public class Scp173PlacingTantrumEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp173PlacingTantrumEventArgs(Scp173TantrumAbility tantrumAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(tantrumAbility.Owner);
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
}