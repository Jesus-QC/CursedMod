using InventorySystem.Items;
using InventorySystem.Items.MicroHID;

namespace CursedMod.Features.Wrappers.Inventory.Items.MicroHID;

public class CursedMicroHidItem : CursedItem
{
    public MicroHIDItem MicroHidBase { get; }
    
    internal CursedMicroHidItem(MicroHIDItem itemBase) : base(itemBase)
    {
        MicroHidBase = itemBase;
    }

    public float Readiness => MicroHidBase.Readiness;

    public byte EnergyToByte => MicroHidBase.EnergyToByte;

    public void Recharge() => MicroHidBase.Recharge();

    public void SendStatus(HidStatusMessageType status, byte code) => MicroHidBase.ServerSendStatus(status, code);

    public void Fire() => MicroHidBase.Fire();

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
}