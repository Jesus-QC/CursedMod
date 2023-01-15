using Hazards;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedTantrumHazard : CursedTemporaryHazard
{
    public TantrumEnvironmentalHazard Base { get; }
    
    public CursedTantrumHazard(TantrumEnvironmentalHazard hazard) : base(hazard)
    {
        Base = hazard;
    }

    public bool PlaySizzle
    {
        get => Base.PlaySizzle;
        set => Base.PlaySizzle = value;
    }
    
    public float ExplosionDistance => Base._explodeDistance;
}