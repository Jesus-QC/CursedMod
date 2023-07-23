// -----------------------------------------------------------------------
// <copyright file="CursedScp244Item.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Usables.Scp244;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables.UsableItems;

public class CursedScp244Item : CursedUsableItem
{
    public CursedScp244Item(Scp244Item scp244Item) 
        : base(scp244Item)
    {
        Scp244Base = scp244Item;
    }
    
    public Scp244Item Scp244Base { get; }

    public bool IsPrimed
    {
        get => Scp244Base._primed;
        set => Scp244Base._primed = value;
    }
}