// -----------------------------------------------------------------------
// <copyright file="CursedFlashlightItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Flashlight;
using Utils.Networking;

namespace CursedMod.Features.Wrappers.Inventory.Items.Flashlight;

public class CursedFlashlightItem : CursedItem
{
    internal CursedFlashlightItem(FlashlightItem itemBase)
        : base(itemBase)
    {
        FlashlightBase = itemBase;
    }
    
    public FlashlightItem FlashlightBase { get; }

    public bool IsEmittingLight
    {
        get => FlashlightBase.IsEmittingLight;
        set
        {
            FlashlightBase.IsEmittingLight = value;
            new FlashlightNetworkHandler.FlashlightMessage(Base.ItemSerial, value).SendToAuthenticated();
        }
    }
}