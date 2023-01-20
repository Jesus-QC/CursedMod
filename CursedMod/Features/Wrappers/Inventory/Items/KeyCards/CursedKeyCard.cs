using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using InventorySystem.Items.Keycards;

namespace CursedMod.Features.Wrappers.Inventory.Items.Keycards;

public class CursedKeyCard : CursedItem
{
    public KeycardItem KeyCardBase { get; }
    
    internal CursedKeyCard(KeycardItem itemBase) : base(itemBase)
    {
        KeyCardBase = itemBase;
    }

    public KeycardPermissions Permissions
    {
        get => KeyCardBase.Permissions;
        set => KeyCardBase.Permissions = value;
    }
}