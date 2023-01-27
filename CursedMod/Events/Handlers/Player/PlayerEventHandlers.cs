using CursedMod.Events.Arguments.Player;

namespace CursedMod.Events.Handlers.Player;

public static class PlayerEventHandlers
{
    public static event EventManager.CursedEventHandler<PlayerJoinedEventArgs> PlayerJoined;

    public static void OnPlayerJoined(PlayerJoinedEventArgs args)
    {
        PlayerJoined.InvokeEvent(args);
    }
    
    public static event EventManager.CursedEventHandler<PlayerDisconnectedEventArgs> PlayerDisconnected;

    public static void OnPlayerDisconnected(PlayerDisconnectedEventArgs args)
    {
        PlayerDisconnected.InvokeEvent(args);
    }
}