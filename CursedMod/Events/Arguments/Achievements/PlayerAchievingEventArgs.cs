// -----------------------------------------------------------------------
// <copyright file="PlayerAchievingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Achievements;
using CursedMod.Features.Wrappers.Player;
using Mirror;

namespace CursedMod.Events.Arguments.Achievements;

public class PlayerAchievingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; set; }

    public AchievementName Achievement { get; set; }

    public PlayerAchievingEventArgs(NetworkConnection connection, AchievementName achievement)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(connection.identity);
        Achievement = achievement;
    }
}