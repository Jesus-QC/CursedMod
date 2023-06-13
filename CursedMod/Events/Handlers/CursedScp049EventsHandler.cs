// -----------------------------------------------------------------------
// <copyright file="CursedScp049EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp049;

namespace CursedMod.Events.Handlers;

public static class CursedScp049EventsHandler
{
    public static event CursedEventManager.CursedEventHandler<Scp049StartingResurrectionEventArgs> StartingResurrection;
    
    public static event CursedEventManager.CursedEventHandler<Scp049ResurrectingPlayerEventArgs> ResurrectingPlayer;
    
    public static event CursedEventManager.CursedEventHandler<Scp049UsingSenseAbilityEventArgs> UsingSenseAbility;
    
    public static event CursedEventManager.CursedEventHandler<Scp049UsingCallAbilityEventArgs> UsingCallAbility; 

    internal static void OnStartingResurrection(Scp049StartingResurrectionEventArgs args)
    {
        StartingResurrection.InvokeEvent(args);
    }
    
    internal static void OnResurrectingPlayer(Scp049ResurrectingPlayerEventArgs args)
    {
        ResurrectingPlayer.InvokeEvent(args);
    }
    
    internal static void OnUsingSenseAbility(Scp049UsingSenseAbilityEventArgs args)
    {
        UsingSenseAbility.InvokeEvent(args);
    }
    
    internal static void OnUsingCallAbility(Scp049UsingCallAbilityEventArgs args)
    {
        UsingCallAbility.InvokeEvent(args);
    }
}