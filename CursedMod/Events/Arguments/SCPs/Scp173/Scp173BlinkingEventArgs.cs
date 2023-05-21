﻿// -----------------------------------------------------------------------
// <copyright file="Scp173BlinkingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp173;

namespace CursedMod.Events.Arguments.SCPs.Scp173;

public class Scp173BlinkingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp173BlinkingEventArgs(Scp173BlinkTimer blinkTimer)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(blinkTimer._fpcModule._role._owner);
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
}