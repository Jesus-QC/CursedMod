using CursedMod.Features.Wrappers.Player.Dummies;

namespace CursedMod.Features.Wrappers.Server;

public static class CursedServer
{
    private static CursedDummy _local;
    
    public static CursedDummy LocalPlayer => _local ??= new CursedDummy(ReferenceHub.HostHub);
    
    
}