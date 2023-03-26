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

namespace CursedMod.Events.Arguments.Items;

public class PlayerThrowingItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerThrowingItemEventArgs(ThrowableItem throwable, ThrowableItem.ProjectileSettings projectileSettings, bool fullForce)
    {
        IsAllowed = true;
        Item = CursedThrowableItem.Get(throwable);
        Player = Item.Owner;
        ProjectileSettings = projectileSettings;
        FullForce = fullForce;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public CursedThrowableItem Item { get; }

    public ThrowableItem.ProjectileSettings ProjectileSettings { get; set; }
    
    public bool FullForce { get; set; }
}