// -----------------------------------------------------------------------
// <copyright file="Scp096ChargingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class Scp096ChargingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp096ChargingEventArgs(Scp096ChargeAbility chargeAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(chargeAbility.Owner);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
}