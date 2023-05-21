// -----------------------------------------------------------------------
// <copyright file="CursedScp1576Item.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Usables.Scp1576;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Items.Usables.UsableItems;

public class CursedScp1576Item : CursedUsableItem
{
    public CursedScp1576Item(Scp1576Item scp1576Item)
        : base(scp1576Item)
    {
        Scp1576Base = scp1576Item;
    }
    
    public Scp1576Item Scp1576Base { get; }
    
    public Scp1576Playback PlaybackTemplate => Scp1576Base.PlaybackTemplate;
    
    public void StopTransmission() => Scp1576Base.ServerStopTransmitting();
}