using CursedMod.Features.Wrappers.Player.Dummies;
using Mirror.LiteNetLib4Mirror;

namespace CursedMod.Features.Wrappers.Server;

public static class CursedServer
{
    private static CursedDummy _local;
    
    public static CursedDummy LocalPlayer => _local ??= new CursedDummy(ReferenceHub.HostHub);

    public static ushort Port
    {
        get => ServerStatic.ServerPort;
        set => ServerStatic.ServerPort = value;
    }

    public static bool ForwardPorts
    {
        get => LiteNetLib4MirrorTransport.Singleton.useUpnP;
        set => LiteNetLib4MirrorTransport.Singleton.useUpnP = value;
    }

    public static int MaxPlayerSlots
    {
        get => CustomNetworkManager.slots;
        set => CustomNetworkManager.slots = value;
    }

    public static int MaxReservedSlots
    {
        get => CustomNetworkManager.reservedSlots;
        set => CustomNetworkManager.reservedSlots = value;
    }

    public static bool Modded
    {
        get => CustomNetworkManager.Modded;
        set => CustomNetworkManager.Modded = value;
    }

    public static bool HeavilyModded
    {
        get => CustomNetworkManager.HeavilyModded;
        set => CustomNetworkManager.HeavilyModded = value;
    }
}