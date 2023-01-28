using CursedMod.Events.Arguments.Items;

namespace CursedMod.Events.Handlers.Items;

public static class ItemsEventHandler
{
    public static event EventManager.CursedEventHandler<PlayerDroppingItemEventArgs> PlayerDroppingItem;

    public static void OnPlayerDroppingItem(PlayerDroppingItemEventArgs args)
    {
        PlayerDroppingItem.InvokeEvent(args);
    }
}