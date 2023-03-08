// -----------------------------------------------------------------------
// <copyright file="ItemsEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Items;

namespace CursedMod.Events.Handlers.Items;

public static class ItemsEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerDroppingItemEventArgs> PlayerDroppingItem;

    public static event EventManager.CursedEventHandler<PlayerCancellingThrowEventArgs> PlayerCancellingThrow;
    
    internal static void OnPlayerDroppingItem(PlayerDroppingItemEventArgs args)
    {
        PlayerDroppingItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerCancellingThrow(PlayerCancellingThrowEventArgs args)
    {
        PlayerCancellingThrow.InvokeEvent(args);
    }
}