using CursedMod.Features.Wrappers.Player;
using PluginAPI.Core;

namespace CursedMod.Events.Handlers.Round;

public static class RoundEventHandlers
{
    public static event EventManager.CursedEventHandler RoundStarted;

    public static void OnRoundStarted()
    {
        RoundStarted.InvokeEvent();
    }
    
    public static event EventManager.CursedEventHandler WaitingForPlayers;

    public static void OnWaitingForPlayers()
    {
        WaitingForPlayers.InvokeEvent();
    }
    
    public static event EventManager.CursedEventHandler RestartingRound;

    public static void OnRestartingRound()
    {
        RestartingRound.InvokeEvent();
    }
}