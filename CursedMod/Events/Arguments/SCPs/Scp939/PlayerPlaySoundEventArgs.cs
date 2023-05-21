// -----------------------------------------------------------------------
// <copyright file="PlayerPlaySoundEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class PlayerPlaySoundEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerPlaySoundEventArgs(EnvironmentalMimicry environmentalMimicry)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(environmentalMimicry.Owner);
        Option = environmentalMimicry._syncOption;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public byte Option { get; }
}