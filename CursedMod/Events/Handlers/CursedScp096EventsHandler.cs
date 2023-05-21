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
    public static event EventManager.CursedEventHandler<PlayerPryGateEventArgs> PlayerPryGate;
    
    public static event EventManager.CursedEventHandler<PlayerTryNotToCryEventArgs> PlayerTryNotToCry;
    
    public static event EventManager.CursedEventHandler<PlayerChargingEventArgs> PlayerCharging;
    
    public static event EventManager.CursedEventHandler<PlayerEnragingEventArgs> PlayerEnraging;
    
    public static event EventManager.CursedEventHandler<PlayerEndEnrageEventArgs> PlayerEndEnrage; 
    
    public static event EventManager.CursedEventHandler<PlayerAddTargetEventArgs> PlayerAddTarget;
    
    public static event EventManager.CursedEventHandler<PlayerRemoveTargetEventArgs> PlayerRemoveTarget;

    internal static void OnPlayerPryGate(PlayerPryGateEventArgs ev)
    {
        PlayerPryGate.InvokeEvent(ev);
    }
    
    internal static void OnPlayerTryNotToCry(PlayerTryNotToCryEventArgs args)
    {
        PlayerTryNotToCry.InvokeEvent(args);
    }
    
    internal static void OnPlayerCharging(PlayerChargingEventArgs args)
    {
        PlayerCharging.InvokeEvent(args);
    }
    
    internal static void OnPlayerEnraging(PlayerEnragingEventArgs args)
    {
        PlayerEnraging.InvokeEvent(args);
    }
    
    internal static void OnPlayerEndEnrage(PlayerEndEnrageEventArgs args)
    {
        PlayerEndEnrage.InvokeEvent(args);
    }
    
    internal static void OnPlayerAddTarget(PlayerAddTargetEventArgs args)
    {
        PlayerAddTarget.InvokeEvent(args);
    }
    
    internal static void OnPlayerRemoveTarget(PlayerRemoveTargetEventArgs args)
    {
        PlayerRemoveTarget.InvokeEvent(args);
    }
}