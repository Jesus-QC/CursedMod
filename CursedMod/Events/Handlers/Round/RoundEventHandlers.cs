// -----------------------------------------------------------------------
// <copyright file="RoundEventHandlers.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CursedMod.Events.Handlers.Round;

public static class RoundEventHandlers
{
    public static event EventManager.CursedEventHandler RoundStarted;
    
    public static event EventManager.CursedEventHandler WaitingForPlayers;
    
    public static event EventManager.CursedEventHandler RestartingRound;
    
    public static event EventManager.CursedEventHandler RoundEnded;

    public static void OnRoundStarted()
    {
        RoundStarted.InvokeEvent();
    }

    public static void OnWaitingForPlayers()
    {
        WaitingForPlayers.InvokeEvent();
    }

    public static void OnRestartingRound()
    {
        RestartingRound.InvokeEvent();
    }

    public static void OnRoundEnded()
    {
        RoundEnded.InvokeEvent();
    }
}