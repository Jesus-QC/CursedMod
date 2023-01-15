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

        List<CursedLockerChamber> chamberList = new List<CursedLockerChamber>();
        foreach (var chamber in Base.Chambers)
            chamberList.Add(new CursedLockerChamber(chamber));

        Chambers = chamberList.ToArray();
    }

    public CursedLockerChamber[] Chambers { get; }

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

    public CursedLocker Create(LockerType lockerType, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        GameObject prefab = Object.Instantiate(CursedPrefabManager.Lockers[lockerType], position, Quaternion.Euler(rotation));

        prefab.transform.localScale = scale;

        NetworkServer.Spawn(prefab);
        return new CursedLocker(prefab.GetComponent<Locker>());
    }
}
