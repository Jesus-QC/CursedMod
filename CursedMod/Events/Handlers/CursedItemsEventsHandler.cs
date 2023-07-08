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
    public static event CursedEventManager.CursedEventHandler<PlayerPickingUpItemEventArgs> PlayerPickingUpItem;
    
    public static event CursedEventManager.CursedEventHandler<PlayerPickedUpItemEventArgs> PlayerPickedUpItem;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDroppingItemEventArgs> PlayerDroppingItem;

    public static event CursedEventManager.CursedEventHandler<PlayerCancellingThrowEventArgs> PlayerCancellingThrow;
    
    public static event CursedEventManager.CursedEventHandler<PlayerCancellingUsableEventArgs> PlayerCancellingUsable;
    
    public static event CursedEventManager.CursedEventHandler<PlayerUsingItemEventArgs> PlayerUsingItem;

    public static event CursedEventManager.CursedEventHandler<PlayerUsedItemEventArgs> PlayerUsedItem;
    
    public static event CursedEventManager.CursedEventHandler<PlayerThrowingItemEventArgs> PlayerThrowingItem;
    
    public static event CursedEventManager.CursedEventHandler<PlayerShootingEventArgs> PlayerShooting;

    public static event CursedEventManager.CursedEventHandler<PlayerReloadingWeaponEventArgs> PlayerReloadingWeapon;
    
    public static event CursedEventManager.CursedEventHandler<PlayerUnloadingWeaponEventArgs> PlayerUnloadingWeapon;
    
    public static event CursedEventManager.CursedEventHandler<PlayerAimingEventArgs> PlayerAiming;
    
    public static event CursedEventManager.CursedEventHandler<PlayerDryfiringWeaponEventArgs> PlayerDryfiringWeapon;
    
    public static event CursedEventManager.CursedEventHandler<PlayerToggleWeaponFlashlightEventArgs> PlayerToggleWeaponFlashlight;
    
    public static event CursedEventManager.CursedEventHandler<PlayerInspectingWeaponEventArgs> PlayerInspectingWeapon; 
    
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

    internal static void OnPlayerReloadingWeapon(PlayerReloadingWeaponEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerReloadingWeapon.InvokeEvent(args);
    }
    
    internal static void OnPlayerUnloadingWeapon(PlayerUnloadingWeaponEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerUnloadingWeapon.InvokeEvent(args);
    }
    
    internal static void OnPlayerAiming(PlayerAimingEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerAiming.InvokeEvent(args);
    }
    
    internal static void OnPlayerDryfiringWeapon(PlayerDryfiringWeaponEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerDryfiringWeapon.InvokeEvent(args);
    }
    
    internal static void OnPlayerToggleWeaponFlashlight(PlayerToggleWeaponFlashlightEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerToggleWeaponFlashlight.InvokeEvent(args);
    }
    
    internal static void OnPlayerInspectingWeapon(PlayerInspectingWeaponEventArgs args)
    {
        if (!args.Player.CheckPlayer())
            return;
        
        PlayerInspectingWeapon.InvokeEvent(args);
    }
}
