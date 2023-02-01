// -----------------------------------------------------------------------
// <copyright file="CursedMicroHIDItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.MicroHID;

namespace CursedMod.Features.Wrappers.Inventory.Items.MicroHID;

public class CursedMicroHidItem : CursedItem
{
    internal CursedMicroHidItem(MicroHIDItem itemBase)
        : base(itemBase)
    {
        MicroHidBase = itemBase;
    }
    
    public MicroHIDItem MicroHidBase { get; }

    public float Readiness => MicroHidBase.Readiness;

    public byte EnergyToByte => MicroHidBase.EnergyToByte;

    public float RemainingEnergy
    {
        get => MicroHidBase.RemainingEnergy;
        set
        {
            MicroHidBase.RemainingEnergy = value;
            SendStatus(HidStatusMessageType.EnergySync, EnergyToByte);
        }
    }

    public HidState State
    {
        get => MicroHidBase.State;
        set
        {
            MicroHidBase.State = value;
            MicroHidBase.ServerSendStatus(HidStatusMessageType.State, (byte)value);
        }
    }
    
    public void Recharge() => MicroHidBase.Recharge();

    public void SendStatus(HidStatusMessageType status, byte code) => MicroHidBase.ServerSendStatus(status, code);

    public void Fire() => MicroHidBase.Fire();
}