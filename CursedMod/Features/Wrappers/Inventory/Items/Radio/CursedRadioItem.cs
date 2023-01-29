// -----------------------------------------------------------------------
// <copyright file="CursedRadioItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Radio;

namespace CursedMod.Features.Wrappers.Inventory.Items.Radio;

public class CursedRadioItem : CursedItem
{
    internal CursedRadioItem(RadioItem itemBase)
        : base(itemBase)
    {
        RadioBase = itemBase;
    }
    
    public RadioItem RadioBase { get; }

    public bool IsUsable => RadioBase.IsUsable;

    public byte BatteryPercent
    {
        get => RadioBase.BatteryPercent;
        set => RadioBase.BatteryPercent = value;
    }

    public RadioMessages.RadioRangeLevel RangeLevel
    {
        get => RadioBase.RangeLevel;
        set => RadioBase._rangeId = (byte)value;
    }
    
    public void ProcessCommand(RadioMessages.RadioCommand command) => RadioBase.ServerProcessCmd(command);
}