// -----------------------------------------------------------------------
// <copyright file="PlayerRemoveMimicPointEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class PlayerRemoveMimicPointEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerRemoveMimicPointEventArgs(MimicPointController mimicPointController)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(mimicPointController.Owner);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
}