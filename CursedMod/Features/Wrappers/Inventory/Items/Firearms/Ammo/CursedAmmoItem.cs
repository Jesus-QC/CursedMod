using InventorySystem.Items;
using InventorySystem.Items.Firearms.Ammo;

namespace CursedMod.Features.Wrappers.Inventory.Items.Firearms.Ammo;

public class CursedAmmoItem : CursedItem
{
    public AmmoItem AmmoBase { get; }
    
    internal CursedAmmoItem(AmmoItem itemBase) : base(itemBase)
    {
        AmmoBase = itemBase;
    }

    public int UnitPrice
    {
        get => AmmoBase.UnitPrice;
        set => AmmoBase.UnitPrice = value;
    }
}