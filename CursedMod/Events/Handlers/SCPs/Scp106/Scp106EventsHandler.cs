// -----------------------------------------------------------------------
// <copyright file="Scp106EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp106;

namespace CursedMod.Events.Handlers.SCPs.Scp106;

public static class Scp106EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerSubmergingEventArgs> PlayerSubmerging;
    
    public static event EventManager.CursedEventHandler<PlayerExitingSubmergeEventArgs> PlayerExitingSubmerge; 

    public static event EventManager.CursedEventHandler<PlayerStalkingEventArgs> PlayerStalking;

    internal static void OnPlayerStartSubmerging(PlayerSubmergingEventArgs args)
    {
        PlayerSubmerging.InvokeEvent(args);
    }
    
    internal static void OnPlayerExitingSubmerge(PlayerExitingSubmergeEventArgs args)
    {
        PlayerExitingSubmerge.InvokeEvent(args);
    }
    
    internal static void OnPlayerStalking(PlayerStalkingEventArgs args)
    {
        PlayerStalking.InvokeEvent(args);
    }
}