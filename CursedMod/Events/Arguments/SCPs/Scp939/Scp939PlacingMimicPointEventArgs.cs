// -----------------------------------------------------------------------
// <copyright file="Scp939PlacingMimicPointEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939PlacingMimicPointEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp939PlacingMimicPointEventArgs(MimicPointController mimicPointController)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(mimicPointController.Owner);
        Position = mimicPointController._syncPos.Position;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public Vector3 Position { get; }
}