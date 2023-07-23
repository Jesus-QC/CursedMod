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
    public static event CursedEventManager.CursedEventHandler<Scp079UsingBlackoutRoomAbilityEventArgs> UsingBlackoutRoomAbility;
    
    public static event CursedEventManager.CursedEventHandler<Scp079UsingBlackoutZoneAbilityEventArgs> UsingBlackoutZoneAbility;
    
    public static event CursedEventManager.CursedEventHandler<Scp079ChangingCameraEventArgs> ChangingCamera;
    
    public static event CursedEventManager.CursedEventHandler<Scp079MovingElevatorEventArgs> MovingElevator;
    
    public static event CursedEventManager.CursedEventHandler<Scp079UsingLockdownAbilityEventArgs> UsingLockdownAbility;

    public static event CursedEventManager.CursedEventHandler<Scp079CancellingLockdownEventArgs> CancellingLockdown; 
    
    public static event CursedEventManager.CursedEventHandler<Scp079LevelingUpEventArgs> LevelingUp;
    
    public static event CursedEventManager.CursedEventHandler<Scp079GainingExperienceEventArgs> GainingExperience;
    
    public static event CursedEventManager.CursedEventHandler<Scp079UsingTeslaEventArgs> UsingTesla;
    
    public static event CursedEventManager.CursedEventHandler<Scp079UsingPingAbilityEventArgs> UsingPingAbility; 
    
    public static event CursedEventManager.CursedEventHandler<Scp079ChangingDoorLockEventArgs> ChangingDoorLock;
    
    public static event CursedEventManager.CursedEventHandler<Scp079ChangingDoorStateEventArgs> ChangingDoorState;

    internal static void OnUsingBlackoutRoomAbility(Scp079UsingBlackoutRoomAbilityEventArgs args)
    {
        UsingBlackoutRoomAbility.InvokeEvent(args);
    }
    
    internal static void OnUsingBlackoutZoneAbility(Scp079UsingBlackoutZoneAbilityEventArgs args)
    {
        UsingBlackoutZoneAbility.InvokeEvent(args);
    }
    
    internal static void OnChangingCamera(Scp079ChangingCameraEventArgs args)
    {
        ChangingCamera.InvokeEvent(args);
    }
    
    internal static void OnMovingElevator(Scp079MovingElevatorEventArgs args)
    {
        MovingElevator.InvokeEvent(args);
    }
    
    internal static void OnUsingLockdownAbility(Scp079UsingLockdownAbilityEventArgs args)
    {
        UsingLockdownAbility.InvokeEvent(args);
    }

    internal static void OnCancellingLockdown(Scp079CancellingLockdownEventArgs args)
    {
        CancellingLockdown.InvokeEvent(args);
    }
    
    internal static void OnLevelingUp(Scp079LevelingUpEventArgs args)
    {
        LevelingUp.InvokeEvent(args);
    }
    
    internal static void OnGainingExperience(Scp079GainingExperienceEventArgs args)
    {
        GainingExperience.InvokeEvent(args);
    }
    
    internal static void OnUsingTesla(Scp079UsingTeslaEventArgs args)
    {
        UsingTesla.InvokeEvent(args);
    }
    
    internal static void OnUsingPingAbility(Scp079UsingPingAbilityEventArgs args)
    {
        UsingPingAbility.InvokeEvent(args);
    }
    
    internal static void OnChangingDoorLock(Scp079ChangingDoorLockEventArgs args)
    {
        ChangingDoorLock.InvokeEvent(args);
    }
    
    internal static void OnChangingDoorState(Scp079ChangingDoorStateEventArgs args)
    {
        ChangingDoorState.InvokeEvent(args);
    }
}