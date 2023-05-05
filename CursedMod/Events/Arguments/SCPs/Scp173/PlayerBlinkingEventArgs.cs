// -----------------------------------------------------------------------
// <copyright file="PlayerBlinkingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.SCPs.Scp173;

public class PlayerBlinkingEventArgs : EventArgs, ICursedPlayerEvent, ICursedCancellableEvent
{
    public PlayerBlinkingEventArgs(ReferenceHub hub, List<CursedPlayer> targets)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(hub);
        Targets = targets;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public List<CursedPlayer> Targets { get; }
}