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
    public static event EventManager.CursedEventHandler<PlayerPickingUpItemEventArgs> PlayerPickingUpItem;
    
    public static event EventManager.CursedEventHandler<PlayerPickedUpItemEventArgs> PlayerPickedUpItem;
    
    public static event EventManager.CursedEventHandler<PlayerDroppingItemEventArgs> PlayerDroppingItem;

    public static event EventManager.CursedEventHandler<PlayerCancellingThrowEventArgs> PlayerCancellingThrow;
    
    public static event EventManager.CursedEventHandler<PlayerCancellingUsableEventArgs> PlayerCancellingUsable;
    
    public static event EventManager.CursedEventHandler<PlayerUsingItemEventArgs> PlayerUsingItem;

    public static event EventManager.CursedEventHandler<PlayerUsedItemEventArgs> PlayerUsedItem;
    
    public static event EventManager.CursedEventHandler<PlayerThrowingItemEventArgs> PlayerThrowingItem;
    
    internal static void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs args)
    {
        PlayerPickingUpItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs args)
    {
        PlayerPickedUpItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerDroppingItem(PlayerDroppingItemEventArgs args)
    {
        PlayerDroppingItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerCancellingThrow(PlayerCancellingThrowEventArgs args)
    {
        PlayerCancellingThrow.InvokeEvent(args);
    }
    
    internal static void OnPlayerCancellingUsable(PlayerCancellingUsableEventArgs args)
    {
        PlayerCancellingUsable.InvokeEvent(args);
    }
    
    internal static void OnPlayerUsingItem(PlayerUsingItemEventArgs args)
    {
        PlayerUsingItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerUsedItem(PlayerUsedItemEventArgs args)
    {
        PlayerUsedItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerThrowingItem(PlayerThrowingItemEventArgs args)
    {
        PlayerThrowingItem.InvokeEvent(args);
    }
}