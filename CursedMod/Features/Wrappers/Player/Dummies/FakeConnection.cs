// -----------------------------------------------------------------------
// <copyright file="FakeConnection.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Mirror;
using PluginAPI.Core;

namespace CursedMod.Features.Wrappers.Player.Dummies;

public class FakeConnection : NetworkConnectionToClient
{
    private static int _idGenerator = int.MaxValue;
 
    public FakeConnection()
        : base(_idGenerator--)
    {
    }
    
    public override string address => "npc";

    public override void Send(ArraySegment<byte> segment, int channelId = 0)
    {
        // ignore
    }

    public override void Disconnect()
    {
        Log.Info("Destroying dummy.");

        CursedDummy.Dictionary.Remove(identity.gameObject.GetComponent<ReferenceHub>());
        NetworkServer.RemovePlayerForConnection(this, true);
    }
}