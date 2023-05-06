// -----------------------------------------------------------------------
// <copyright file="PlayerSensingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Arguments.SCPs.Scp049;

public class PlayerSensingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerSensingEventArgs(Scp049SenseAbility senseAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(senseAbility.Owner);
        Target = CursedPlayer.Get(senseAbility.Target);
        Distance = senseAbility._distanceThreshold;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedPlayer Target { get; }
    
    public float Distance { get; set; }
}