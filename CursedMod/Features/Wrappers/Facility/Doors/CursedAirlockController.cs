// -----------------------------------------------------------------------
// <copyright file="CursedAirlockController.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Interactables.Interobjects;

namespace CursedMod.Features.Wrappers.Facility.Doors;

public class CursedAirlockController
{
    internal CursedAirlockController(AirlockController airlockController)
    {
        Base = airlockController;
    }
    
    public AirlockController Base { get; }

    public CursedDoor DoorA
    {
        get => CursedDoor.Get(Base._doorA);
        set => Base._doorA = value.Base;
    }
    
    public CursedDoor DoorB
    {
        get => CursedDoor.Get(Base._doorB);
        set => Base._doorB = value.Base;
    }
    
    public bool AirlockDisabled
    {
        get => Base.AirlockDisabled;
        set => Base.AirlockDisabled = value;
    }

    public bool DoorsLocked
    {
        get => Base._doorsLocked;
        set => Base._doorsLocked = value;
    }

    public float LockdownCooldown
    {
        get => Base._lockdownCooldown;
        set => Base._lockdownCooldown = value;
    }

    public float LockdownDuration
    {
        get => Base._lockdownDuration;
        set => Base._lockdownDuration = value;
    }

    public bool ReadyToUse
    {
        get => Base._readyToUse;
        set => Base._readyToUse = value;
    }

    public static CursedAirlockController Get(AirlockController controller) => new (controller);
    
    public void SendAlarm() => Base.RpcAlarm();

    public void ToggleAirlock() => Base.ToggleAirlock();
}