using CursedMod.Features.Enums;
using Hazards;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedSinkholeHazard : CursedEnvironmentalHazard
{
    public SinkholeEnvironmentalHazard Base { get; }
    
    public CursedSinkholeHazard(SinkholeEnvironmentalHazard hazard) : base(hazard)
    {
        Base = hazard;
        HazardType = EnvironmentalHazardType.Sinkhole;
    }
}