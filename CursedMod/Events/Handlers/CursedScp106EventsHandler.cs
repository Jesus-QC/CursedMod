// -----------------------------------------------------------------------
// <copyright file="CursedScp106EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp106;

namespace CursedMod.Events.Handlers;

public static class CursedScp106EventsHandler
{
    public static event CursedEventManager.CursedEventHandler<Scp106SubmergingEventArgs> Submerging;
    
    public static event CursedEventManager.CursedEventHandler<Scp106ExitingSubmergenceEventArgs> ExitingSubmergence; 

    public static event CursedEventManager.CursedEventHandler<Scp106UsingStalkAbilityEventArgs> UsingStalkAbility;

    internal static void OnSubmerging(Scp106SubmergingEventArgs args)
    {
        Submerging.InvokeEvent(args);
    }
    
    internal static void OnExitingSubmergence(Scp106ExitingSubmergenceEventArgs args)
    {
        ExitingSubmergence.InvokeEvent(args);
    }
    
    internal static void OnUsingStalkAbility(Scp106UsingStalkAbilityEventArgs args)
    {
        UsingStalkAbility.InvokeEvent(args);
    }
}