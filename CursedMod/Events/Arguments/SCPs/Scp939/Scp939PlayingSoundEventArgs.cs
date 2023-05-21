// -----------------------------------------------------------------------
// <copyright file="Scp939PlayingSoundEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939PlayingSoundEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp939PlayingSoundEventArgs(EnvironmentalMimicry environmentalMimicry)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(environmentalMimicry.Owner);
        Option = environmentalMimicry._syncOption;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public byte Option { get; }
}