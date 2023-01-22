using InventorySystem.Items;
using InventorySystem.Items.Armor;

namespace CursedMod.Features.Wrappers.Inventory.Items.Armor;

public class CursedBodyArmorItem : CursedItem
{
    public BodyArmor ArmorBase { get; }
    
    internal CursedBodyArmorItem(BodyArmor itemBase) : base(itemBase)
    {
        ArmorBase = itemBase;
    }

    public int HelmetEfficacy
    {
        get => ArmorBase.HelmetEfficacy;
        set => ArmorBase.HelmetEfficacy = value;
    }

    public int VestEfficacy
    {
        get => ArmorBase.VestEfficacy;
        set => ArmorBase.VestEfficacy = value;
    }

    public BodyArmor.ArmorAmmoLimit[] AmmoLimits
    {
        get => ArmorBase.AmmoLimits;
        set => ArmorBase.AmmoLimits = value;
    }

    public BodyArmor.ArmorCategoryLimitModifier[] CategoryLimits
    {
        get => ArmorBase.CategoryLimits;
        set => ArmorBase.CategoryLimits = value;
    }

    public float StaminaUseMultiplier
    {
        get => ArmorBase._staminaUseMultiplier;
        set => ArmorBase._staminaUseMultiplier = value;
    }
    
    public float MovementSpeedMultiplier
    {
        get => ArmorBase._movementSpeedMultiplier;
        set => ArmorBase._movementSpeedMultiplier = value;
    }
}