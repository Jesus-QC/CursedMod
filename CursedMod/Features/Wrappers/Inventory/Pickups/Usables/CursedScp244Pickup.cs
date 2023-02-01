// -----------------------------------------------------------------------
// <copyright file="CursedScp244Pickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Usables.Scp244;
using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Pickups.Usables;

public class CursedScp244Pickup : CursedPickup
{
    internal CursedScp244Pickup(Scp244DeployablePickup itemPickupBase)
        : base(itemPickupBase)
    {
        Scp244Base = itemPickupBase;
    }
    
    public Scp244DeployablePickup Scp244Base { get; }

    public bool ModelDestroyed => Scp244Base.ModelDestroyed;

    public byte SizePercent
    {
        get => Scp244Base._syncSizePercent;
        set => Scp244Base.Network_syncSizePercent = value;
    }

    public Scp244State State
    {
        get => Scp244Base.State;
        set => Scp244Base.State = value;
    }

    public float Health
    {
        get => Scp244Base._health;
        set => Scp244Base._health = value;
    }
    
    public bool Damage(float damage, DamageHandlerBase type, Vector3 exactHitPosition) => Scp244Base.Damage(damage, type, exactHitPosition);
}