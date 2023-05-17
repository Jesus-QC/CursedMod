// -----------------------------------------------------------------------
// <copyright file="CursedScp079EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp079;

namespace CursedMod.Events.Handlers;

public static class CursedScp079EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerBlackoutRoomEventArgs> PlayerBlackoutRoom;
    
    public static event EventManager.CursedEventHandler<PlayerBlackoutZoneEventArgs> PlayerBlackoutZone;
    
    public static event EventManager.CursedEventHandler<PlayerChangeCameraEventArgs> PlayerChangeCamera;
    
    public static event EventManager.CursedEventHandler<PlayerMoveElevatorEventArgs> PlayerMoveElevator;
    
    public static event EventManager.CursedEventHandler<PlayerUseLockdownEventArgs> PlayerUseLockdown;

    public static event EventManager.CursedEventHandler<PlayerCancelLockdownEventArgs> PlayerCancelLockdown; 
    
    public static event EventManager.CursedEventHandler<PlayerLevelUpEventArgs> PlayerLevelUp;
    
    public static event EventManager.CursedEventHandler<PlayerGainExperienceEventArgs> PlayerGainExperience;
    
    public static event EventManager.CursedEventHandler<PlayerUseTeslaEventArgs> PlayerUseTesla;
    
    public static event EventManager.CursedEventHandler<PlayerPingEventArgs> PlayerPing; 

    internal static void OnPlayerBlackoutRoom(PlayerBlackoutRoomEventArgs args)
    {
        PlayerBlackoutRoom.InvokeEvent(args);
    }
    
    internal static void OnPlayerBlackoutZone(PlayerBlackoutZoneEventArgs args)
    {
        PlayerBlackoutZone.InvokeEvent(args);
    }
    
    internal static void OnPlayerChangeCamera(PlayerChangeCameraEventArgs args)
    {
        PlayerChangeCamera.InvokeEvent(args);
    }
    
    internal static void OnPlayerMoveElevator(PlayerMoveElevatorEventArgs args)
    {
        PlayerMoveElevator.InvokeEvent(args);
    }
    
    internal static void OnPlayerUseLockdown(PlayerUseLockdownEventArgs args)
    {
        PlayerUseLockdown.InvokeEvent(args);
    }

    internal static void OnPlayerCancelLockdown(PlayerCancelLockdownEventArgs args)
    {
        PlayerCancelLockdown.InvokeEvent(args);
    }
    
    internal static void OnPlayerLevelUp(PlayerLevelUpEventArgs args)
    {
        PlayerLevelUp.InvokeEvent(args);
    }
    
    internal static void OnPlayerGainExperience(PlayerGainExperienceEventArgs args)
    {
        PlayerGainExperience.InvokeEvent(args);
    }
    
    internal static void OnPlayerUseTesla(PlayerUseTeslaEventArgs args)
    {
        PlayerUseTesla.InvokeEvent(args);
    }
    
    internal static void OnPlayerPing(PlayerPingEventArgs args)
    {
        PlayerPing.InvokeEvent(args);
    }
}