// -----------------------------------------------------------------------
// <copyright file="Scp096CalmingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class Scp096CalmingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp096CalmingEventArgs(Scp096RageManager rageManager, bool clearTime)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(rageManager.Owner);
        ClearTime = clearTime;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public bool ClearTime { get; set; }
}