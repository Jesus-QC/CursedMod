// -----------------------------------------------------------------------
// <copyright file="AchievementsEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Achievements;

namespace CursedMod.Events.Handlers.Achievements;

public static class AchievementsEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerAchievingEventArgs> PlayerAchieving;

    public static void OnPlayerAchieving(PlayerAchievingEventArgs args)
    {
        PlayerAchieving.InvokeEvent(args);
    }
}