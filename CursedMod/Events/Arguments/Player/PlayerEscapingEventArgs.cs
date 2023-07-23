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
using Respawning;

namespace CursedMod.Events.Arguments.Player;

public class PlayerEscapingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerEscapingEventArgs(ReferenceHub hub, RoleTypeId newRole, Escape.EscapeScenarioType escapeScenarioType, SpawnableTeamType team, float tokens)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(hub);
        NewRole = newRole;
        EscapeScenarioType = escapeScenarioType;
        EscapeTime = Player.CurrentRole.ActiveTime;
        TeamReceivingTokens = team;
        Tokens = tokens;
    }

    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public RoleTypeId NewRole { get; set; }
    
    public Escape.EscapeScenarioType EscapeScenarioType { get; set; }
    
    public float EscapeTime { get; }
    
    public SpawnableTeamType TeamReceivingTokens { get; set; }
    
    public float Tokens { get; set; }
}