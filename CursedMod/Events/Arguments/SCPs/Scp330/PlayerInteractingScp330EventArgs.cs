// -----------------------------------------------------------------------
// <copyright file="PlayerInteractingScp330EventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.SCPs.Scp330;

public class PlayerInteractingScp330EventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerInteractingScp330EventArgs(ReferenceHub hub)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(hub);
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
}