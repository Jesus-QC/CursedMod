// -----------------------------------------------------------------------
// <copyright file="CursedWarheadEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Warhead;

namespace CursedMod.Events.Handlers;

public static class CursedWarheadEventsHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerStartingDetonationEventArgs> PlayerStartingDetonation;
    
    public static event CursedEventManager.CursedEventHandler<PlayerCancelingDetonationEventArgs> PlayerCancelingDetonation;
    
    public static event CursedEventManager.CursedEventHandler<WarheadDetonatingEventArgs> WarheadDetonating;

    internal static void OnPlayerStartingDetonation(PlayerStartingDetonationEventArgs args)
    {
        PlayerStartingDetonation.InvokeEvent(args); 
    }
    
    internal static void OnPlayerCancelingDetonation(PlayerCancelingDetonationEventArgs args)
    {
        PlayerCancelingDetonation.InvokeEvent(args); 
    }
    
    internal static void OnWarheadDetonating(WarheadDetonatingEventArgs args)
    {
        WarheadDetonating.InvokeEvent(args); 
    }
}