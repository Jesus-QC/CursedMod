// -----------------------------------------------------------------------
// <copyright file="CursedLightningController.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class CursedLightningController
{
    internal CursedLightningController(RoomLightController controller)
    {
        Base = controller;
    }
    
    public RoomLightController Base { get; }

    public Color Color
    {
        get => Base.NetworkOverrideColor;
        set
        {
            Base.NetworkOverrideColor = value;
            WarheadLightOverride = true;
        }
    }

    public bool WarheadLightOverride
    {
        get => Base.NetworkLightsEnabled;
        set => Base.NetworkLightsEnabled = value;
    }

    public bool LightsEnabled
    {
        get => Base.LightsEnabled;
        set => Base.NetworkLightsEnabled = value;
    }

    public void ResetColor()
    {
        WarheadLightOverride = false;
    }

    public void FlickerLights(float duration)
    {
        Base.ServerFlickerLights(duration);
    }
}