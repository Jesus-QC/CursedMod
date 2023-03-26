// -----------------------------------------------------------------------
// <copyright file="CursedSinkholeHazard.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Enums;
using Hazards;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedSinkholeHazard : CursedEnvironmentalHazard
{
    internal CursedSinkholeHazard(SinkholeEnvironmentalHazard hazard)
        : base(hazard)
    {
        Base = hazard;
        HazardType = EnvironmentalHazardType.Sinkhole;
    }
    
    public SinkholeEnvironmentalHazard Base { get; }
}