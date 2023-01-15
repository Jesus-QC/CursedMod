using CursedMod.Features.Enums;
using MapGeneration.Distributors;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedLocker
{
    public Locker Base { get; }

    public CursedLocker(Locker locker)
    {
        Base = locker;
        Chambers = Base.Chambers.Select(chamber => new CursedLockerChamber(chamber));
    }

    public IEnumerable<CursedLockerChamber> Chambers { get; }

    public Vector3 Position
    {
        get => Base.transform.position;
        set => Base.transform.position = value;
    }

    public Quaternion Rotation
    {
        get => Base.transform.rotation;
        set => Base.transform.rotation = value;
    }

    public Vector3 Scale
    {
        get => Base.transform.localScale;
        set => Base.transform.localScale = value;
    }

    public CursedLocker Create(LockerType lockerType, Vector3 position, Vector3 rotation, Vector3? scale = null)
    {
        Locker prefab = Object.Instantiate(CursedPrefabManager.Lockers[lockerType], position, Quaternion.Euler(rotation));

        if (scale.HasValue)
            prefab.transform.localScale = scale.Value;
        
        NetworkServer.Spawn(prefab.gameObject);
        return new CursedLocker(prefab);
    }
}
