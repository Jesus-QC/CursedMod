// -----------------------------------------------------------------------
// <copyright file="CursedScp268Item.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CustomPlayerEffects;
using InventorySystem.Items.Usables;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables.UsableItems;

public class CursedScp268Item : CursedUsableItem
{
    public CursedScp268Item(Scp268 scp268)
        : base(scp268)
    {
        Scp268Base = scp268;
    }
    
    public Scp268 Scp268Base { get; }
    
    public bool IsWorn
    {
        get => Scp268Base.IsWorn;
        set => Scp268Base.IsWorn = value;
    }
    
    public Invisible Effect => Scp268Base.Effect;

    public void SetItemState(bool state) => Scp268Base.SetState(state);
}