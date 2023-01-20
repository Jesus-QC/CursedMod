using InventorySystem.Items.Firearms;

namespace CursedMod.Features.Wrappers.Inventory.Items.Firearms;

public class CursedAutomaticFirearm : CursedFirearm
{
    public AutomaticFirearm AutomaticFirearmBase { get; }
    
    internal CursedAutomaticFirearm(AutomaticFirearm itemBase) : base(itemBase)
    {
        AutomaticFirearmBase = itemBase;
    }

    //
    public float FireRate
    {
        get => AutomaticFirearmBase._fireRate;
        set => AutomaticFirearmBase._fireRate = value;
    }
}