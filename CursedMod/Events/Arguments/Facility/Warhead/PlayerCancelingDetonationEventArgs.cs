// -----------------------------------------------------------------------
// <copyright file="PlayerCancelingDetonationEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.Facility.Warhead;

public class PlayerCancelingDetonationEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerCancelingDetonationEventArgs(ReferenceHub disabler)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(disabler);
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
}