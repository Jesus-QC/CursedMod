namespace CursedMod.Features.Wrappers.Facility;

public static class CursedFacility
{
    private static Broadcast _broadcast;

    public static Broadcast Broadcast => _broadcast ??= Broadcast.Singleton;
}