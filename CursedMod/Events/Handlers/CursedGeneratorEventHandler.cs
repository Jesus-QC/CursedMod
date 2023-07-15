// -----------------------------------------------------------------------
// <copyright file="CursedGeneratorEventHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Facility.Generators;

namespace CursedMod.Events.Handlers;

public static class CursedGeneratorEventHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerOpeningGeneratorEventArgs> OpeningGenerator;

    public static event CursedEventManager.CursedEventHandler<PlayerClosingGeneratorEventArgs> ClosingGenerator;
    
    public static event CursedEventManager.CursedEventHandler<PlayerUnlockingGeneratorEventArgs> UnlockingGenerator;

    public static event CursedEventManager.CursedEventHandler<PlayerActivatingGeneratorEventArgs> ActivatingGenerator;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDeactivatingGeneratorEventArgs> DeactivatingGenerator;

    internal static void OnOpeningGenerator(PlayerOpeningGeneratorEventArgs args)
    {
        OpeningGenerator.InvokeEvent(args);
    }
    
    internal static void OnClosingGenerator(PlayerClosingGeneratorEventArgs args)
    {
        ClosingGenerator.InvokeEvent(args);
    }
    
    internal static void OnUnlockingGenerator(PlayerUnlockingGeneratorEventArgs args)
    {
        UnlockingGenerator.InvokeEvent(args);
    }
    
    internal static void OnActivatingGenerator(PlayerActivatingGeneratorEventArgs args)
    {
        ActivatingGenerator.InvokeEvent(args);
    }
    
    internal static void OnDeactivatingGenerator(PlayerDeactivatingGeneratorEventArgs args)
    {
        DeactivatingGenerator.InvokeEvent(args);
    }
}