using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using InventorySystem.Items.Keycards;

namespace CursedMod.Features.Wrappers.Inventory.Items.KeyCards;

public class CursedKeyCardItem : CursedItem
{
    public KeycardItem KeyCardBase { get; }
    
    internal CursedKeyCardItem(KeycardItem itemBase) : base(itemBase)
    {
        KeyCardBase = itemBase;
    }

    public KeycardPermissions Permissions
    {
        get => KeyCardBase.Permissions;
        set => KeyCardBase.Permissions = value;
    }
}