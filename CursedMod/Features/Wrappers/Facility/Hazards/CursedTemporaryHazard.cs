using Hazards;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedTemporaryHazard : CursedEnvironmentalHazard
{
    public TemporaryHazard TemporaryHazard { get; }
    
    public CursedTemporaryHazard(TemporaryHazard hazard) : base(hazard)
    {
        TemporaryHazard = hazard;
    }

    public float DecaySpeed => TemporaryHazard.DecaySpeed;

    public float HazardDuration => TemporaryHazard.HazardDuration;

    public bool IsDestroyed
    {
        get => TemporaryHazard._destroyed;
        set => TemporaryHazard._destroyed = value;
    }

    public void Destroy() => TemporaryHazard.ServerDestroy();
}