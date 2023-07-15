// -----------------------------------------------------------------------
// <copyright file="PlayerPlacingBloodDecalEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using UnityEngine;

namespace CursedMod.Events.Arguments.Player;

public class PlayerPlacingBloodDecalEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerPlacingBloodDecalEventArgs(ReferenceHub player, ReferenceHub target, Vector3 pos)
    {
        Player = CursedPlayer.Get(player);
        Target = CursedPlayer.Get(target);
        Position = pos;
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }
    
    public CursedPlayer Target { get; }
    
    public Vector3 Position { get; }

    public bool IsAllowed { get; set; }
}