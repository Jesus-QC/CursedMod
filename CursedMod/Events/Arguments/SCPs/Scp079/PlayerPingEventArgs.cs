// -----------------------------------------------------------------------
// <copyright file="PlayerPingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class PlayerPingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerPingEventArgs(Scp079PingAbility pingAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(pingAbility.Owner);
        Index = pingAbility._syncProcessorIndex;
        PowerCost = pingAbility._cost;
        Position = pingAbility._syncPos.Position;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public int Index { get; }
    
    public int PowerCost { get; set; }
    
    public Vector3 Position { get; }
}