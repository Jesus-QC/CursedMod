// -----------------------------------------------------------------------
// <copyright file="CursedLightSource.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AdminToys;
using UnityEngine;

namespace CursedMod.Features.Wrappers.AdminToys;

public class CursedLightSource : CursedAdminToy
{
    internal CursedLightSource(LightSourceToy lightSource)
        : base(lightSource)
    {
        Base = lightSource;
    }

    public LightSourceToy Base { get; }

    public float LightIntensity
    {
        get => Base.LightIntensity;
        set => Base.NetworkLightIntensity = value;
    }

    public float LightRange
    {
        get => Base.LightRange;
        set => Base.NetworkLightRange = value;
    }

    public Color LightColor
    {
        get => Base.LightColor;
        set => Base.NetworkLightColor = value;
    }

    public bool LightShadows
    {
        get => Base.LightShadows;
        set => Base.NetworkLightShadows = value;
    }
    
    public static CursedLightSource Create(Vector3? position = null, Vector3? rotation = null, Vector3? scale = null, float? lightIntensity = null, float? lightRange = null, Color? lightColor = null, bool? lightShadows = null, bool spawn = false)
    {
        LightSourceToy lightSourceToy = Object.Instantiate(CursedPrefabManager.LightSource);
        CursedLightSource light = new (lightSourceToy);
        
        if (position.HasValue)
            light.Position = position.Value;

        if (rotation.HasValue)
            light.Rotation = rotation.Value;

        if (scale.HasValue)
            light.Scale = scale.Value;

        if (lightIntensity.HasValue)
            light.LightIntensity = lightIntensity.Value;

        if (lightRange.HasValue)
            light.LightRange = lightRange.Value;

        if (lightColor.HasValue)
            light.LightColor = lightColor.Value;

        if (lightShadows.HasValue)
            light.LightShadows = lightShadows.Value;

        if (spawn)
            light.Spawn();

        return light;
    }
}