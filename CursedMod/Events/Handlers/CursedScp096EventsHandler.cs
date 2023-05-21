// -----------------------------------------------------------------------
// <copyright file="CursedScp096EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp096;

namespace CursedMod.Events.Handlers;

public static class CursedScp096EventsHandler
{
    public static event EventManager.CursedEventHandler<Scp096PryingGateEventArgs> PryingGate;
    
    public static event EventManager.CursedEventHandler<Scp096TryingNotToCryEventArgs> TryingNotToCry;
    
    public static event EventManager.CursedEventHandler<Scp096ChargingEventArgs> Charging;
    
    public static event EventManager.CursedEventHandler<Scp096EnragingEventArgs> Enraging;
    
    public static event EventManager.CursedEventHandler<Scp096CalmingEventArgs> Calming; 
    
    public static event EventManager.CursedEventHandler<Scp096AddingTargetEventArgs> AddingTarget;
    
    public static event EventManager.CursedEventHandler<Scp096RemovingTargetEventArgs> RemovingTarget;

    internal static void OnPryingGate(Scp096PryingGateEventArgs ev)
    {
        PryingGate.InvokeEvent(ev);
    }
    
    internal static void OnTryingNotToCry(Scp096TryingNotToCryEventArgs args)
    {
        TryingNotToCry.InvokeEvent(args);
    }
    
    internal static void OnCharging(Scp096ChargingEventArgs args)
    {
        Charging.InvokeEvent(args);
    }
    
    internal static void OnEnraging(Scp096EnragingEventArgs args)
    {
        Enraging.InvokeEvent(args);
    }
    
    internal static void OnCalming(Scp096CalmingEventArgs args)
    {
        Calming.InvokeEvent(args);
    }
    
    internal static void OnAddingTarget(Scp096AddingTargetEventArgs args)
    {
        AddingTarget.InvokeEvent(args);
    }
    
    internal static void OnRemovingTarget(Scp096RemovingTargetEventArgs args)
    {
        RemovingTarget.InvokeEvent(args);
    }
}