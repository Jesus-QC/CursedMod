using MapGeneration.Distributors;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedGenerator
{
    public static Scp079Generator Base { get; private set; }

    public static bool IsEngaged
    {
        get => Base.Engaged;
        set => Base.Engaged = value;
    }

    public static bool IsActivating
    {
        get => Base.Activating;
        set => Base.Activating = value;
    }

    public static int RemainingTime => Base.RemainingTime;
    public static float DropdownSpeed => Base.DropdownSpeed;
}