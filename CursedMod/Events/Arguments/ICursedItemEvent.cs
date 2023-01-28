using CursedMod.Features.Wrappers.Inventory.Items;

namespace CursedMod.Events.Arguments;

public interface ICursedItemEvent
{
    public CursedItem Item { get; }
}