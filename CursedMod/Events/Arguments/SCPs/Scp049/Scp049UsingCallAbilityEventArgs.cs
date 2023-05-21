// -----------------------------------------------------------------------
// <copyright file="Scp049UsingCallAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Arguments.SCPs.Scp049;

public class Scp049UsingCallAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp049UsingCallAbilityEventArgs(Scp049CallAbility callAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(callAbility.Owner);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
}