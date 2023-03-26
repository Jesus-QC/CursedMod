// -----------------------------------------------------------------------
// <copyright file="CursedBreakableWindow.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedBreakableWindow
{
    internal CursedBreakableWindow(BreakableWindow window)
    {
        Base = window;
    }
    
    public BreakableWindow Base { get; }

    public Vector3 Position
    {
        get => Base._transform.position;
        set => Base._transform.position = value;
    }

    public Quaternion Rotation
    {
        get => Base._transform.rotation;
        set => Base._transform.rotation = value;
    }

    public bool IsBroken
    {
        get => Base.syncStatus.broken;
        set => Base.syncStatus.broken = value;
    }

    public void Break() => Base.isBroken = true;

    public bool Damage(float damage, DamageHandlerBase damageHandlerBase, Vector3 position) => Base.Damage(damage, damageHandlerBase, position);

    public void ForceDamage(float damage) => Base.ServerDamageWindow(damage);
}