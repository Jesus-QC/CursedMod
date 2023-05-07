// -----------------------------------------------------------------------
// <copyright file="PlayerBloodlustingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.SCPs.Scp0492;

public class PlayerBloodlustingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerBloodlustingEventArgs(CursedPlayer player, CursedPlayer target)
    {
        IsAllowed = true;
        Player = player;
        Target = target;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedPlayer Target { get; }
}