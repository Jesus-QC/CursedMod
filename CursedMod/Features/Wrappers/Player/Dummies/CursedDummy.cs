using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers.Player.Dummies;

public class CursedDummy : CursedPlayer
{
    public new static readonly Dictionary<ReferenceHub, CursedDummy> Dictionary = new ();
    public new static HashSet<CursedDummy> Set => Dictionary.Values.ToHashSet();
    public new static List<CursedDummy> List => Dictionary.Values.ToList();
    public new static int Count => Dictionary.Count;

    public static readonly Dictionary<CursedPlayer, CursedDummy> OwnerDictionary = new ();

    internal CursedDummy(ReferenceHub hub) : base(hub, true)
    {
        if (hub == ReferenceHub.HostHub)
            return;
        
        Dictionary.Add(hub, this);
        CharacterClassManager._privUserId = $"ID_NPC_{hub.PlayerId}";
    }

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
}