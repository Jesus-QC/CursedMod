// -----------------------------------------------------------------------
// <copyright file="Scp096EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp096;

namespace CursedMod.Events.Handlers.SCPs.Scp096;

public static class Scp096EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerPryGateEventArgs> PlayerPryGate;
    
    public static event EventManager.CursedEventHandler<PlayerTryNotToCryEventArgs> PlayerTryNotToCry;
    
    public static event EventManager.CursedEventHandler<PlayerChargingEventArgs> PlayerCharging;
    
    public static event EventManager.CursedEventHandler<PlayerEnragingEventArgs> PlayerEnraging;
    
    public static event EventManager.CursedEventHandler<PlayerEndEnrageEventArgs> PlayerEndEnrage; 

    internal static void OnPlayerPryGate(PlayerPryGateEventArgs ev)
    {
        PlayerPryGate.InvokeEvent(ev);
    }
    
    internal static void OnPlayerTryNotToCry(PlayerTryNotToCryEventArgs ev)
    {
        PlayerTryNotToCry.InvokeEvent(ev);
    }
    
    internal static void OnPlayerCharging(PlayerChargingEventArgs ev)
    {
        PlayerCharging.InvokeEvent(ev);
    }
    
    internal static void OnPlayerEnraging(PlayerEnragingEventArgs ev)
    {
        PlayerEnraging.InvokeEvent(ev);
    }
    
    internal static void OnPlayerEndEnrage(PlayerEndEnrageEventArgs ev)
    {
        PlayerEndEnrage.InvokeEvent(ev);
    }
}