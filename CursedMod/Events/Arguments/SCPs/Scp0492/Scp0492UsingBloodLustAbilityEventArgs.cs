// -----------------------------------------------------------------------
// <copyright file="Scp0492UsingBloodLustAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.SCPs.Scp0492;

public class Scp0492UsingBloodLustAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp0492UsingBloodLustAbilityEventArgs(ReferenceHub player, ReferenceHub target)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(player);
        Target = CursedPlayer.Get(target);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedPlayer Target { get; }
}