// -----------------------------------------------------------------------
// <copyright file="PlayerEatScp330EventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Usables.Scp330;

namespace CursedMod.Events.Arguments.SCPs.Scp330;

public class PlayerEatScp330EventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerEatScp330EventArgs(Scp330Bag bag, ICandy candy)
    {
        IsAllowed = true;
        Scp330Bag = bag;
        Player = CursedPlayer.Get(bag.Owner);
        Candy = candy;
    }
    
    public bool IsAllowed { get; set; }

    public ICandy Candy { get; }

    public CursedPlayer Player { get; }
    
    public Scp330Bag Scp330Bag { get; }
}