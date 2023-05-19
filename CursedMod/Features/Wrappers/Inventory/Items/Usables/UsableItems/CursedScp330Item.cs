// -----------------------------------------------------------------------
// <copyright file="CursedScp330Item.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables.UsableItems;

public class CursedScp330Item : CursedUsableItem
{
    public CursedScp330Item(Scp330Bag scp330Bag)
        : base(scp330Bag)
    {
        Scp330Base = scp330Bag;
    }
    
    public Scp330Bag Scp330Base { get; }

    public int SelectedCandyId
    {
        get => Scp330Base.SelectedCandyId;
        set => Scp330Base.SelectedCandyId = value;
    }
    
    public bool IsCandySelected => SelectedCandyId >= 0 && SelectedCandyId < Candies.Count;
    
    public IReadOnlyCollection<CandyKindID> Candies => Scp330Base.Candies.AsReadOnly();

    public bool AddCandy(CandyKindID candyKindID)
    {
        if (Candies.Count >= 6)
            return false;

        Scp330Base.Candies.Add(candyKindID);
        Scp330Base.ServerRefreshBag();
        return true;
    }

    public bool RemoveCandy(CandyKindID candyKindID, bool removeAll = false)
    {
        if (Scp330Base.Candies.Contains(candyKindID) && !removeAll)
        {
            Scp330Base.TryRemove(Scp330Base.Candies.IndexOf(candyKindID));
        }

        if (removeAll)
        {
            Scp330Base.Candies.RemoveAll(candy => candy == candyKindID);
            Scp330Base.ServerRefreshBag();
        }

        return true;
    }

    public bool DropCandy(CandyKindID candyKindID)
    {
        if (Scp330Base.Candies.Contains(candyKindID))
        {
            Scp330Base.DropCandy(Scp330Base.Candies.IndexOf(candyKindID));
        }

        return true;
    }
}