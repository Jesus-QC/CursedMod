// -----------------------------------------------------------------------
// <copyright file="CursedRespawningEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Respawning;

namespace CursedMod.Events.Handlers;

public static class CursedRespawningEventsHandler
{
    public static event EventManager.CursedEventHandler<RespawningTeamEventArgs> RespawningTeam;

    internal static void OnRespawningTeam(RespawningTeamEventArgs args)
    {
        RespawningTeam.InvokeEvent(args);
    }
}