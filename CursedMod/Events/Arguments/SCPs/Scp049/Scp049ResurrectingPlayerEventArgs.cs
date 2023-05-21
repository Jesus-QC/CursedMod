// -----------------------------------------------------------------------
// <copyright file="Scp049ResurrectingPlayerEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Player.Ragdolls;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Arguments.SCPs.Scp049;

public class Scp049ResurrectingPlayerEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedRagdollEvent
{
    public Scp049ResurrectingPlayerEventArgs(Scp049ResurrectAbility resurrectAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(resurrectAbility.Owner);
        Ragdoll = CursedRagdoll.Get(resurrectAbility.CurRagdoll);
    }

    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedRagdoll Ragdoll { get; }
}