// -----------------------------------------------------------------------
// <copyright file="Scp173EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp173;

namespace CursedMod.Events.Handlers.SCPs.Scp173;

public class Scp173EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerBlinkingEventArgs> PlayerBlinking;
    
    public static event EventManager.CursedEventHandler<PlayerUseBreakneckSpeedEventArgs> PlayerUseBreakneckSpeed;
    
    internal static void OnPlayerBlinking(PlayerBlinkingEventArgs args)
    {
        PlayerBlinking.InvokeEvent(args);
    }
    
    internal static void OnPlayerUseBreakneckSpeed(PlayerUseBreakneckSpeedEventArgs args)
    {
        PlayerUseBreakneckSpeed.InvokeEvent(args);
    }
}