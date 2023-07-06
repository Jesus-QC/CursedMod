// -----------------------------------------------------------------------
// <copyright file="CursedDesyncModule.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CursedMod.Events.Arguments.Player;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player;

public static class CursedDesyncModule
{
    public static readonly Dictionary<CursedPlayer, Vector3> FakedScales = new ();

    public static void ClearCache()
    {
        FakedScales.Clear();
    }

    public static void HandlePlayerConnected(PlayerConnectedEventArgs args)
    {
        foreach (KeyValuePair<CursedPlayer, Vector3> fakedScale in FakedScales)
        {
            args.Player.SendFakeScaleMessage(fakedScale.Key, fakedScale.Value);
        }
    }

    public static void SendFakeScaleMessage(this CursedPlayer target, CursedPlayer player, Vector3 scale)
    {
        try
        {
            NetworkIdentity identity = player.NetworkIdentity;
            NetworkConnection conn = target.NetworkConnection;
        
            if (identity.serverOnly)
                return;

            using NetworkWriterPooled ownerWriter = NetworkWriterPool.Get();
            using NetworkWriterPooled observersWriter = NetworkWriterPool.Get();
            bool isOwner = identity.connectionToClient == conn;
            ArraySegment<byte> spawnMessagePayload = NetworkServer.CreateSpawnMessagePayload(isOwner, identity, ownerWriter, observersWriter);
            Transform transform = identity.transform;
            
            SpawnMessage message = new ()
            {
                netId = identity.netId,
                isLocalPlayer = conn.identity == identity,
                isOwner = isOwner,
                sceneId = identity.sceneId,
                assetId = identity.assetId,
                position = transform.localPosition,
                rotation = transform.localRotation,
                scale = scale,
                payload = spawnMessagePayload,
            };
            
            conn.Send(message);
        }
        catch
        {
            // Safely Ignore
        }
    }
}