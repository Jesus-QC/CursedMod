// -----------------------------------------------------------------------
// <copyright file="CursedDummy.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

/*using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers.Player.Dummies;

public class CursedDummy : CursedPlayer
{
    public new static readonly Dictionary<ReferenceHub, CursedDummy> Dictionary = new ();
    
    internal CursedDummy(ReferenceHub hub) 
        : base(hub)
    {
        if (hub is null)
            return;
        
        Dictionary.Add(hub, this);
        CharacterClassManager._privUserId = $"ID_NPC_{hub.PlayerId}";
    }

    public new static IEnumerable<CursedDummy> Collection => Dictionary.Values;
    
    public new static List<CursedDummy> List => Collection.ToList();
   
    public new static int Count => Dictionary.Count;

    public static CursedDummy Create(string nick = null)
    {
        GameObject ply = Object.Instantiate(NetworkManager.singleton.playerPrefab);

        if (!string.IsNullOrEmpty(nick))
            ply.GetComponent<ReferenceHub>().nicknameSync.Network_myNickSync = nick;
            
        NetworkServer.AddPlayerForConnection(new FakeConnection(), ply);
        return new CursedDummy(ply.GetComponent<ReferenceHub>());
    }

    public void Destroy()
    {
        Dictionary.Remove(ReferenceHub);
        Object.Destroy(GameObject);
    }
}*/