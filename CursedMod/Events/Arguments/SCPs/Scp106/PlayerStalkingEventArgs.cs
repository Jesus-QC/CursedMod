// -----------------------------------------------------------------------
// <copyright file="PlayerStalkingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Events.Arguments.SCPs.Scp106;

public class PlayerStalkingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerStalkingEventArgs(Scp106StalkAbility stalkAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(stalkAbility.Owner);
        MinimumVigor = Scp106StalkAbility.MinVigorToSubmerge;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public float MinimumVigor { get; set; }
}