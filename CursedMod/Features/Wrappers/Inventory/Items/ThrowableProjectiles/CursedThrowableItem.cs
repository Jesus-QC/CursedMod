// -----------------------------------------------------------------------
// <copyright file="CursedThrowableItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Server;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Items.ThrowableProjectiles;

public class CursedThrowableItem : CursedItem
{
    internal CursedThrowableItem(ThrowableItem itemBase)
        : base(itemBase)
    {
        ThrowableBase = itemBase;
    }
    
    public ThrowableItem ThrowableBase { get; }

    public bool ReadyToThrow => ThrowableBase.ReadyToThrow;
    
    public bool ReadyToCancel => ThrowableBase.ReadyToThrow;

    public void PropelBody(Rigidbody rigidbody, Vector3 torque, Vector3 relativeVelocity, float forceAmount, float upwardFactor)
        => ThrowableBase.PropelBody(rigidbody, torque, relativeVelocity, forceAmount, upwardFactor);

    public void Throw(float forceAmount, float upwardFactor, Vector3 torque, Vector3 startVel)
        => ThrowableBase.ServerThrow(forceAmount, upwardFactor, torque, startVel);

    public void SpawnCharged(Vector3 pos, float fuseTime = -1)
    {
        if (Base is not ThrowableItem item)
            return;

        ThrownProjectile thrownProjectile = Object.Instantiate(item.Projectile, pos, Quaternion.identity);

        if (fuseTime > 0)
            ((TimeGrenade)thrownProjectile)._fuseTime = fuseTime;
        
        PickupSyncInfo pickupSyncInfo = new (item.ItemTypeId, pos, Quaternion.identity, item.Weight, item.ItemSerial) { Locked = true };
        PickupSyncInfo pickupSyncInfo2 = pickupSyncInfo;
        thrownProjectile.NetworkInfo = pickupSyncInfo2;
        thrownProjectile.PreviousOwner = Owner?.Footprint ?? CursedServer.LocalPlayer.Footprint;
        NetworkServer.Spawn(thrownProjectile.gameObject);
        ItemPickupBase itemPickupBase = thrownProjectile;
        pickupSyncInfo = default;
        itemPickupBase.InfoReceived(pickupSyncInfo, pickupSyncInfo2);
        thrownProjectile.ServerActivate();
    }
}