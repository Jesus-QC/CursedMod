// -----------------------------------------------------------------------
// <copyright file="CursedHazardsEventHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Hazards;

namespace CursedMod.Events.Handlers;

public static class CursedHazardsEventHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerEnteringHazardEventArgs> EnteringHazard;

    public static event CursedEventManager.CursedEventHandler<PlayerExitingHazardEventArgs> ExitingHazard;
    
    public static event CursedEventManager.CursedEventHandler<PlayerStayingOnHazardEventArgs> StayingOnHazard; 

    internal static void OnEnteringHazard(PlayerEnteringHazardEventArgs args)
    {
        EnteringHazard.InvokeEvent(args);
    }
    
    internal static void OnExitingHazard(PlayerExitingHazardEventArgs args)
    {
        ExitingHazard.InvokeEvent(args);
    }
    
    internal static void OnStayingOnHazard(PlayerStayingOnHazardEventArgs args)
    {
        StayingOnHazard.InvokeEvent(args);
    }
}