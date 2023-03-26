// -----------------------------------------------------------------------
// <copyright file="CursedTimedGrenadePickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Footprinting;
using InventorySystem.Items.ThrowableProjectiles;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.ThrowableProjectiles;

public class CursedTimedGrenadePickup : CursedPickup
{
    internal CursedTimedGrenadePickup(TimedGrenadePickup itemBase)
        : base(itemBase)
    {
        TimedGrenadeBase = itemBase;
    }
    
    public TimedGrenadePickup TimedGrenadeBase { get; }

    public Footprint Attacker => TimedGrenadeBase._attacker;
}