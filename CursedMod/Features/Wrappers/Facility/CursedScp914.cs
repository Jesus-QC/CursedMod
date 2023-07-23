// -----------------------------------------------------------------------
// <copyright file="CursedScp914.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Interactables.Interobjects.DoorUtils;
using Scp914;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedScp914
{
    public static Scp914KnobSetting KnobSetting
    {
        get => Scp914Controller.Singleton._knobSetting;
        set => Scp914Controller.Singleton.Network_knobSetting = value;
    }

    public static Scp914Mode Mode
    {
        get => Scp914Controller.Singleton._configMode.Value;
        set => Scp914Controller.Singleton._configMode.Value = value;
    }

    public static bool IsUpgrading
    {
        get => Scp914Controller.Singleton._isUpgrading;
        set => Scp914Controller.Singleton._isUpgrading = value;
    }

    public static float RemainingCooldown
    {
        get => Scp914Controller.Singleton._remainingCooldown;
        set => Scp914Controller.Singleton._remainingCooldown = value;
    }

    public static void PlayKnobChangeSound() => Scp914Controller.Singleton.RpcPlaySound(0);

    public static void PlayUpgradingSound() => Scp914Controller.Singleton.RpcPlaySound(1);

    public static void StartUpgrading()
    {
        Scp914Controller controller = Scp914Controller.Singleton;
        RemainingCooldown = controller._totalSequenceTime;
        controller._itemsAlreadyUpgraded = false;
        IsUpgrading = true;
        PlayUpgradingSound();
    }

    public static void OpenDoors() => SetDoorStatus(true);
    
    public static void CloseDoors() => SetDoorStatus(false);
    
    public static void SetDoorStatus(bool status)
    {
        DoorVariant[] doors = Scp914Controller.Singleton._doors;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].NetworkTargetState = status;
        }
    }
    
    public static void UpgradeItemsAndPlayers()
    {
        Scp914Controller controller = Scp914Controller.Singleton;
        Vector3 position = controller.IntakeChamber.position;
        Scp914Upgrader.Upgrade(Physics.OverlapBox(position, controller.IntakeChamberSize), controller.OutputChamber.position - position, controller._configMode.Value, controller._knobSetting);
    }
}