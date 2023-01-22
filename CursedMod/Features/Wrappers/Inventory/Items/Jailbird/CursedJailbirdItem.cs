using InventorySystem.Items;
using InventorySystem.Items.Jailbird;

namespace CursedMod.Features.Wrappers.Inventory.Items.Jailbird;

public class CursedJailbirdItem : CursedItem
{
    public JailbirdItem JailbirdBase { get; }
    
    internal CursedJailbirdItem(JailbirdItem itemBase) : base(itemBase)
    {
        JailbirdBase = itemBase;
    }

    public int TotalChargesPerformed
    {
        get => JailbirdBase.TotalChargesPerformed;
        set => JailbirdBase.TotalChargesPerformed = value;
    }

    public void Attack() => JailbirdBase.ServerAttack(null);

    public JailbirdHitreg HitRegion => JailbirdBase._hitreg;

    public double ChargeResetTime
    {
        get => JailbirdBase._chargeResetTime;
        set => JailbirdBase._chargeResetTime = value;
    }

    public bool ChargeLoading
    {
        get => JailbirdBase._chargeLoading;
        set => JailbirdBase._chargeLoading = value;
    }

    public bool Charging
    {
        get => JailbirdBase._charging;
        set => JailbirdBase._charging = value;
    }

    public float MeleeDelay
    {
        get => JailbirdBase._meleeDelay;
        set => JailbirdBase._meleeDelay = value;
    }
    
    public float MeleeCooldown
    {
        get => JailbirdBase._meleeCooldown;
        set => JailbirdBase._meleeCooldown = value;
    }

    public float ChargeDuration
    {
        get => JailbirdBase._chargeDuration;
        set => JailbirdBase._chargeDuration = value;
    }

    public float ChargeMovementMultiplier
    {
        get => JailbirdBase._chargeMovementSpeedMultiplier;
        set => JailbirdBase._chargeMovementSpeedMultiplier = value;
    }

    public float ChargeMovementLimiter
    {
        get => JailbirdBase._chargeMovementSpeedLimiter;
        set => JailbirdBase._chargeMovementSpeedLimiter = value;
    }
}