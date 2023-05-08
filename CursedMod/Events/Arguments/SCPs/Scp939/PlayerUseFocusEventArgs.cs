﻿// -----------------------------------------------------------------------
// <copyright file="PlayerUseFocusEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class PlayerUseFocusEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerUseFocusEventArgs(Scp939FocusKeySync focusKeySync, bool focusState)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(focusKeySync.Owner);
        IsFocusing = focusState;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public bool IsFocusing { get; set; }
}