// -----------------------------------------------------------------------
// <copyright file="PlayerEscapingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles;

namespace CursedMod.Events.Arguments.Player;

public class PlayerEscapingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerEscapingEventArgs(ReferenceHub hub, RoleTypeId newRole, Escape.EscapeScenarioType escapeScenarioType)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(hub);
        NewRole = newRole;
        EscapeScenarioType = escapeScenarioType;
        EscapeTime = Player.CurrentRole.ActiveTime;
    }

    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public RoleTypeId NewRole { get; set; }
    
    public Escape.EscapeScenarioType EscapeScenarioType { get; set; }
    
    public float EscapeTime { get; }
}