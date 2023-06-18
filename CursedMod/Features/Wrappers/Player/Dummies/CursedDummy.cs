// -----------------------------------------------------------------------
// <copyright file="CursedDummy.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers.Player.Dummies;

public static class CursedDummy
{
    public static readonly Dictionary<ReferenceHub, CursedPlayer> Dictionary = new ();
    
    public static IEnumerable<CursedPlayer> Collection => Dictionary.Values;
    
    public static List<CursedPlayer> List => Collection.ToList();
   
    public static int Count => Dictionary.Count;

    public static CursedPlayer Create(string nick = null)
    {
        GameObject ply = Object.Instantiate(NetworkManager.singleton.playerPrefab);
        ReferenceHub hub = ply.GetComponent<ReferenceHub>();
        
        if (!string.IsNullOrEmpty(nick))
            hub.nicknameSync.Network_myNickSync = nick;
            
        NetworkServer.AddPlayerForConnection(new FakeConnection(), ply);
        
        CursedPlayer player = new (hub);
        Dictionary.Add(hub, player);
        
        player.CharacterClassManager._privUserId = "ID_NPC";
       
        return player;
    }

    public static bool IsDummy(this ReferenceHub hub) => Dictionary.ContainsKey(hub);

    public static void DestroyDummy(this CursedPlayer dummy)
    {
        Dictionary.Remove(dummy.ReferenceHub);
        NetworkServer.Destroy(dummy.GameObject);
    }
}