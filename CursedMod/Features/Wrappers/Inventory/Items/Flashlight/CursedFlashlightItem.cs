using InventorySystem.Items;
using InventorySystem.Items.Flashlight;
using Utils.Networking;

namespace CursedMod.Features.Wrappers.Inventory.Items.Flashlight;

public class CursedFlashlightItem : CursedItem
{
    public FlashlightItem FlashlightBase { get; }
    
    internal CursedFlashlightItem(FlashlightItem itemBase) : base(itemBase)
    {
        FlashlightBase = itemBase;
    }

    public bool IsEmittingLight
    {
        get => FlashlightBase.IsEmittingLight;
        set
        {
            FlashlightBase.IsEmittingLight = value;
            new FlashlightNetworkHandler.FlashlightMessage(Base.ItemSerial, value).SendToAuthenticated();
        }
    }
}