// -----------------------------------------------------------------------
// <copyright file="CursedJailbirdItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Jailbird;

namespace CursedMod.Features.Wrappers.Inventory.Items.Jailbird;

public class CursedJailbirdItem : CursedItem
{
    internal CursedJailbirdItem(JailbirdItem itemBase)
        : base(itemBase)
    {
        JailbirdBase = itemBase;
    }
    
    public JailbirdItem JailbirdBase { get; }

    public int TotalChargesPerformed
    {
        get => JailbirdBase.TotalChargesPerformed;
        set => JailbirdBase.TotalChargesPerformed = value;
    }

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
    
    public void Attack() => JailbirdBase.ServerAttack(null);
}