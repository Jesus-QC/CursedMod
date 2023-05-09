// -----------------------------------------------------------------------
// <copyright file="PlayerEnragingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class PlayerEnragingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerEnragingEventArgs(Scp096RageManager rageManager, float rageTime)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(rageManager.Owner);
        RageTime = rageTime;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public float RageTime { get; set; }
}