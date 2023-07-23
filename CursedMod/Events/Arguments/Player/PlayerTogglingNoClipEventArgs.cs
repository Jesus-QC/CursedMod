// -----------------------------------------------------------------------
// <copyright file="PlayerTogglingNoClipEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.Player;

public class PlayerTogglingNoClipEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    // Runs to everyone even if they don't have noclip perms, documentation should explicitly state this.
    public PlayerTogglingNoClipEventArgs(ReferenceHub hub)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(hub);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
}