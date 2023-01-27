namespace CursedMod.Events.Handlers.Round;

public static class RoundEventHandlers
{
    public static event EventManager.CursedEventHandler RoundStarted;

    public static void OnRoundStarted()
    {
        RoundStarted.InvokeEvent();
    }
}