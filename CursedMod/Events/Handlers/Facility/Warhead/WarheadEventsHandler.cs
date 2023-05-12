// -----------------------------------------------------------------------
// <copyright file="WarheadEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Warhead;

namespace CursedMod.Events.Handlers.Facility.Warhead;

public static class WarheadEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerStartingDetonationEventArgs> PlayerStartingDetonation;
    
    public static event EventManager.CursedEventHandler<PlayerCancelingDetonationEventArgs> PlayerCancelingDetonation;

    internal static void OnPlayerStartingDetonation(PlayerStartingDetonationEventArgs args)
    {
        PlayerStartingDetonation.InvokeEvent(args); 
    }
    
    internal static void OnPlayerCancelingDetonation(PlayerCancelingDetonationEventArgs args)
    {
        PlayerCancelingDetonation.InvokeEvent(args); 
    }
}