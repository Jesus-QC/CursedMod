// -----------------------------------------------------------------------
// <copyright file="CursedPlayerHitBox.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player;

public class CursedPlayerHitBox
{
    public HitboxIdentity Base { get; }

    public CursedPlayer Owner => CursedPlayer.Get(Base.TargetHub);

    public HitboxType HitBoxType
    {
        get => Base.HitboxType;
        set => Base._dmgMultiplier = value;
    }

    public bool Damage(float damage, DamageHandlerBase handler, Vector3 exactPosition) => Base.Damage(damage, handler, exactPosition);

    public void DisableColliders() => Base.SetColliders(false);
    
    public void EnableColliders() => Base.SetColliders(true);
}