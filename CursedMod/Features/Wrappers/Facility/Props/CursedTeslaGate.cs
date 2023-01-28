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

    public void StartIdling() => Base.RpcDoIdle();

    public void StopIdling() => Base.RpcDoneIdling();

    public void InstantBurst() => Base.RpcInstantBurst();

    public void PlayAnimation() => Base.RpcPlayAnimation();

    public float TriggerRange
    {
        get => Base.sizeOfTrigger;
        set => Base.sizeOfTrigger = value;
    }
}