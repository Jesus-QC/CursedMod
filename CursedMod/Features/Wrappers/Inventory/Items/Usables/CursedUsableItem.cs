using InventorySystem.Items.Usables;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables;

public class CursedUsableItem : CursedItem
{
    public UsableItem UsableBase { get; }
    
    internal CursedUsableItem(UsableItem itemBase) : base(itemBase)
    {
        UsableBase = itemBase;
    }

    public bool CanStartUsing
    {
        get => UsableBase.CanStartUsing;
        set => UsableBase.CanStartUsing = value;
    }

    public bool IsUsing
    {
        get => UsableBase.IsUsing;
        set => UsableBase.IsUsing = value;
    }

    public void SetPersonalCooldown(float seconds) => UsableBase.ServerSetPersonalCooldown(seconds);
    
    public void SetGlobalCooldown(float seconds) => UsableBase.ServerSetGlobalItemCooldown(seconds);

    public float RemainingCooldown
    {
        get => UsableBase.RemainingCooldown;
        set => UsableBase.RemainingCooldown = value;
    }

    public float UseTime
    {
        get => UsableBase.UseTime;
        set => UsableBase.UseTime = value;
    }

    public float MaxCancellableTime
    {
        get => UsableBase.MaxCancellableTime;
        set => UsableBase.MaxCancellableTime = value;
    }
}