// -----------------------------------------------------------------------
// <copyright file="CursedTemporaryHazard.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Enums;
using Hazards;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedTemporaryHazard : CursedEnvironmentalHazard
{
    internal CursedTemporaryHazard(TemporaryHazard hazard)
        : base(hazard)
    {
        TemporaryHazard = hazard;
        HazardType = EnvironmentalHazardType.Temporary;
    }
    
    public TemporaryHazard TemporaryHazard { get; }

    public float DecaySpeed => TemporaryHazard.DecaySpeed;

    public float HazardDuration => TemporaryHazard.HazardDuration;

    public bool IsDestroyed
    {
        get => TemporaryHazard._destroyed;
        set => TemporaryHazard._destroyed = value;
    }

    public void Destroy() => TemporaryHazard.ServerDestroy();
}