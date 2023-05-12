// -----------------------------------------------------------------------
// <copyright file="PlayerStartingDetonationEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.Facility.Warhead;

public class PlayerStartingDetonationEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerStartingDetonationEventArgs(bool isAutomatic, bool suppressSubtitles, ReferenceHub trigger)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(trigger);
        IsAutomatic = isAutomatic;
        SuppressSubtitles = suppressSubtitles;
    }
    
    public bool IsAllowed { get; set; }
    
    public bool IsAutomatic { get; set; }
    
    public bool SuppressSubtitles { get; set; }
    
    public CursedPlayer Player { get; }
}