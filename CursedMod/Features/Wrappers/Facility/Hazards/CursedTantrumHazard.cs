// -----------------------------------------------------------------------
// <copyright file="CursedTantrumHazard.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Enums;
using Hazards;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedTantrumHazard : CursedTemporaryHazard
{
    internal CursedTantrumHazard(TantrumEnvironmentalHazard hazard)
        : base(hazard)
    {
        Base = hazard;
        HazardType = EnvironmentalHazardType.Tantrum;
    }
    
    public TantrumEnvironmentalHazard Base { get; }

    public bool PlaySizzle
    {
        get => Base.PlaySizzle;
        set => Base.PlaySizzle = value;
    }
    
    public float ExplosionDistance => Base._explodeDistance;
}