// -----------------------------------------------------------------------
// <copyright file="Scp939TogglingFocusAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939TogglingFocusAbilityEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp939TogglingFocusAbilityEventArgs(Scp939FocusAbility focusAbility, bool focusState)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(focusAbility.Owner);
        NewState = focusState;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public bool NewState { get; set; }
}