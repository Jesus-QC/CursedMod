// -----------------------------------------------------------------------
// <copyright file="Scp079LevelingUpEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079LevelingUpEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079LevelingUpEventArgs(Scp079TierManager tierManager, int newLevelIndex)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(tierManager.Owner);
        NewLevel = newLevelIndex;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public int NewLevel { get; set; }
}