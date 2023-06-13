// -----------------------------------------------------------------------
// <copyright file="CursedAchievementsEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Achievements;

namespace CursedMod.Events.Handlers;

public static class CursedAchievementsEventsHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerAchievingEventArgs> PlayerAchieving;

    internal static void OnPlayerAchieving(PlayerAchievingEventArgs args)
    {
        PlayerAchieving.InvokeEvent(args);
    }
}