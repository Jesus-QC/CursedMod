// -----------------------------------------------------------------------
// <copyright file="CursedScp330EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp330;

namespace CursedMod.Events.Handlers;

public static class CursedScp330EventsHandler
{
    public static event CursedEventManager.CursedEventHandler<PlayerInteractingScp330EventArgs> PlayerInteractingScp330;
    
    public static event CursedEventManager.CursedEventHandler<PlayerEatingScp330EventArgs> PlayerEatingScp330; 

    internal static void OnPlayerInteractingScp330(PlayerInteractingScp330EventArgs args)
    {
        PlayerInteractingScp330.InvokeEvent(args);
    }
    
    internal static void OnPlayerEatingScp330(PlayerEatingScp330EventArgs args)
    {
        PlayerEatingScp330.InvokeEvent(args);
    }
}