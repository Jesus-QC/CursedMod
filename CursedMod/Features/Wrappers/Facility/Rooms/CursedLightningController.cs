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
    internal CursedLightningController(FlickerableLightController controller)
    {
        Base = controller;
    }
    
    public FlickerableLightController Base { get; }
    
    public CursedRoom Room { get; } // todo
    
    public Color Color
    {
        get => Base._warheadLightColor;
        set
        {
            Base.Network_warheadLightColor = value;
            WarheadLightOverride = true;
        }
    }

    public bool WarheadLightOverride
    {
        get => Base._warheadLightOverride;
        set => Base.Network_warheadLightOverride = value;
    }

    public bool LightsEnabled
    {
        get => Base.LightsEnabled;
        set => Base.NetworkLightsEnabled = value;
    }

    public float LightIntensityMultiplier
    {
        get => Base._lightIntensityMultiplier;
        set => Base.Network_lightIntensityMultiplier = value;
    }

    public void ResetColor()
    {
        Color = FlickerableLightController.DefaultWarheadColor;
        WarheadLightOverride = false;
    }
}