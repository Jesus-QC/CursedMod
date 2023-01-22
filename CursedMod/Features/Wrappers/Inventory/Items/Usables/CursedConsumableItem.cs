using InventorySystem.Items.Usables;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables;

public class CursedConsumableItem : CursedUsableItem
{
    public Consumable BaseConsumable { get; }
    
    internal CursedConsumableItem(Consumable itemBase) : base(itemBase)
    {
        BaseConsumable = itemBase;
    }

    public bool ActivationReady => BaseConsumable.ActivationReady;

    public void Consume() => BaseConsumable.ActivateEffects();
}