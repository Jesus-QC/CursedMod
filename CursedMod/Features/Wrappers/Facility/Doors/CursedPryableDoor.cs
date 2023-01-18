using CursedMod.Features.Enums;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;

namespace CursedMod.Features.Wrappers.Facility.Doors;

public class CursedPryableDoor : CursedDoor
{
    public PryableDoor PryableBase { get; }
    
    internal CursedPryableDoor(PryableDoor door) : base(door)
    {
        PryableBase = door;
        DoorType = DoorType.Pryable;
    }

    public bool TryPry() => PryableBase.TryPryGate(ReferenceHub.HostHub);

    public float RemainingPryCooldown
    {
        get => PryableBase._remainingPryCooldown;
        set => PryableBase._remainingPryCooldown = value;
    }

    public float PryCooldown
    {
        get => PryableBase._pryAnimDuration;
        set => PryableBase._pryAnimDuration = value;
    }

    public bool IsScp106Passable => PryableBase.IsScp106Passable;
}