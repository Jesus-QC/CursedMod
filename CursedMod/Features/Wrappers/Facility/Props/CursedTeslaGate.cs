using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedTeslaGate
{
    public TeslaGate Base { get; }

    public CursedTeslaGate(TeslaGate gate)
    {
        Base = gate;
    }

    public bool IsInRange(Vector3 position) => Base.InRange(position);
}