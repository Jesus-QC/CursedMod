using System;
using Mirror;
using PluginAPI.Core;

namespace CursedMod.Features.Wrappers.Player.Dummies;

public class FakeConnection : NetworkConnectionToClient
{
    private static int _idGenerator = int.MaxValue;
    
    public override void Send(ArraySegment<byte> segment, int channelId = 0)
    {
        // ignore
    }

    public override void Disconnect()
    {
        Log.Info("Destroying dummy."); 
        CursedDummy.Dictionary[identity.gameObject.GetComponent<ReferenceHub>()].Destroy();
    }

    public override string address { get; } = "npc";

    public FakeConnection() : base(_idGenerator--, true, 0) { }
}