// -----------------------------------------------------------------------
// <copyright file="PlayerUseTeslaEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Props;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class PlayerUseTeslaEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerUseTeslaEventArgs(Scp079TeslaAbility teslaAbility, CursedTeslaGate teslaGate)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(teslaAbility.Owner);
        PowerCost = teslaAbility._cost;
        TeslaGate = teslaGate;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedTeslaGate TeslaGate { get; }
    
    public int PowerCost { get; set; }
}