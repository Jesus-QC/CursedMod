// -----------------------------------------------------------------------
// <copyright file="CursedScp173EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp173;

namespace CursedMod.Events.Handlers;

public static class CursedScp173EventsHandler
{
    public static event EventManager.CursedEventHandler<Scp173BlinkingEventArgs> Blinking;
    
    public static event EventManager.CursedEventHandler<Scp173TogglingBreakneckSpeedAbilityEventArgs> TogglingBreakneckSpeedAbility;
    
    public static event EventManager.CursedEventHandler<Scp173PlacingTantrumEventArgs> PlacingTantrum;

    internal static void OnBlinking(Scp173BlinkingEventArgs args)
    {
        Blinking.InvokeEvent(args);
    }
    
    internal static void OnTogglingBreakneckSpeedAbility(Scp173TogglingBreakneckSpeedAbilityEventArgs args)
    {
        TogglingBreakneckSpeedAbility.InvokeEvent(args);
    }
    
    internal static void OnPlacingTantrum(Scp173PlacingTantrumEventArgs args)
    {
        PlacingTantrum.InvokeEvent(args);
    }
}