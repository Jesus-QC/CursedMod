// -----------------------------------------------------------------------
// <copyright file="PlayerThrowingItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Inventory.Items.ThrowableProjectiles;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.ThrowableProjectiles;
using UnityEngine;

namespace CursedMod.Events.Arguments.Items;

public class PlayerThrowingItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerThrowingItemEventArgs(ThrowableItem throwable, float forceAmount, float upwardFactor, Vector3 torque, Vector3 startVel)
    {
        IsAllowed = true;
        Item = CursedThrowableItem.Get(throwable);
        Player = Item.Owner;
        ForceAmount = forceAmount;
        UpwardFactor = upwardFactor;
        Torque = torque;
        StartVelocity = startVel;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public CursedThrowableItem Item { get; }

    public float ForceAmount { get; set; }
    
    public float UpwardFactor { get; set; }
    
    public Vector3 Torque { get; set; }
    
    public Vector3 StartVelocity { get; set; }
}