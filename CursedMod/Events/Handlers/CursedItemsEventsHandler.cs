// -----------------------------------------------------------------------
// <copyright file="CursedItemsEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Items;

namespace CursedMod.Events.Handlers;

public static class CursedItemsEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerPickingUpItemEventArgs> PlayerPickingUpItem;
    
    public static event EventManager.CursedEventHandler<PlayerPickedUpItemEventArgs> PlayerPickedUpItem;
    
    public static event EventManager.CursedEventHandler<PlayerDroppingItemEventArgs> PlayerDroppingItem;

    public static event EventManager.CursedEventHandler<PlayerCancellingThrowEventArgs> PlayerCancellingThrow;
    
    public static event EventManager.CursedEventHandler<PlayerCancellingUsableEventArgs> PlayerCancellingUsable;
    
    public static event EventManager.CursedEventHandler<PlayerUsingItemEventArgs> PlayerUsingItem;

    public static event EventManager.CursedEventHandler<PlayerUsedItemEventArgs> PlayerUsedItem;
    
    public static event EventManager.CursedEventHandler<PlayerThrowingItemEventArgs> PlayerThrowingItem;
    
    public static event EventManager.CursedEventHandler<PlayerShootingEventArgs> PlayerShooting;
    
    internal static void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerPickingUpItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerPickedUpItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerDroppingItem(PlayerDroppingItemEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerDroppingItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerCancellingThrow(PlayerCancellingThrowEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerCancellingThrow.InvokeEvent(args);
    }
    
    internal static void OnPlayerCancellingUsable(PlayerCancellingUsableEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerCancellingUsable.InvokeEvent(args);
    }
    
    internal static void OnPlayerUsingItem(PlayerUsingItemEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerUsingItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerUsedItem(PlayerUsedItemEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerUsedItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerThrowingItem(PlayerThrowingItemEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerThrowingItem.InvokeEvent(args);
    }
    
    internal static void OnPlayerShooting(PlayerShootingEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerShooting.InvokeEvent(args);
    }
}