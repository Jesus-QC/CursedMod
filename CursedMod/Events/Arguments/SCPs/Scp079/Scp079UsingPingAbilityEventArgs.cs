// -----------------------------------------------------------------------
// <copyright file="Scp079UsingPingAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using RelativePositioning;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079UsingPingAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079UsingPingAbilityEventArgs(Scp079PingAbility pingAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(pingAbility.Owner);
        Index = pingAbility._syncProcessorIndex;
        Position = pingAbility._syncPos;
        Normal = pingAbility._syncNormal;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public int Index { get; }
    
    public RelativePosition Position { get; set; }
    
    public Vector3 Normal { get; set; }
}