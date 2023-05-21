// -----------------------------------------------------------------------
// <copyright file="Scp939PlacingAmnesticCloudEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939PlacingAmnesticCloudEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp939PlacingAmnesticCloudEventArgs(Scp939AmnesticCloudAbility amnesticCloudAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(amnesticCloudAbility.Owner);
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
}