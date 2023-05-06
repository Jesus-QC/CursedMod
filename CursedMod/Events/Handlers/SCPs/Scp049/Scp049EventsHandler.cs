// -----------------------------------------------------------------------
// <copyright file="Scp049EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp049;

namespace CursedMod.Events.Handlers.SCPs.Scp049;

public static class Scp049EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerRevivingEventArgs> PlayerReviving;
    
    internal static void OnPlayerReviving(PlayerRevivingEventArgs args)
    {
        PlayerReviving.InvokeEvent(args);
    }
}