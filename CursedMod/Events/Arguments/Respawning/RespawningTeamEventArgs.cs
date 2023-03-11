// -----------------------------------------------------------------------
// <copyright file="RespawningTeamEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Respawning;

namespace CursedMod.Events.Arguments.Respawning;

public class RespawningTeamEventArgs : EventArgs, ICursedCancellableEvent
{
    public RespawningTeamEventArgs()
    {
        IsAllowed = true;
    }
    
    public bool IsAllowed { get; set; }
    
    public SpawnableTeamType TeamSpawning
    {
        get => RespawnManager.Singleton.NextKnownTeam;
        set => RespawnManager.Singleton.NextKnownTeam = value;
    }
}