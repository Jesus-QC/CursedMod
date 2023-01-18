using CursedMod.Features.Wrappers.Inventory;
using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedLockerChamber
{
    public LockerChamber Base { get; }

    public CursedLockerChamber(LockerChamber lockerChamber)
    {
        Base = lockerChamber;
        Content = Base._content.Select(pickup => new CursedPickup(pickup));
    }

    public IEnumerable<CursedPickup> Content { get; }

    public bool CanInteract => Base.CanInteract;

    public GameObject GameObject => Base.gameObject;

    public Transform Transform => Base.transform;

    public KeycardPermissions RequiredPermissions
    {
        get => Base.RequiredPermissions;
        set => Base.RequiredPermissions = value;
    }

    public bool IsOpen
    {
        get => Base.IsOpen;
        set => Base.IsOpen = value;
    }

    public Vector3 Position
    {
        get => Transform.position;
        set => Transform.position = value;
    }

    public Quaternion Rotation
    {
        get => Transform.rotation;
        set => Transform.rotation = value;
    }

    public void SpawnItem(ItemType itemType, int amount) => Base.SpawnItem(itemType, amount);

    public void SetDoor(bool status, AudioClip clip) => Base.SetDoor(status, clip);

    public void PlayDeniedSound(AudioClip clip) => Base.PlayDenied(clip);

    public override string ToString() => $"{nameof(CursedLockerChamber)}: Opened: {IsOpen} | CanInteract: {CanInteract} | Position: {Position} | Rotation: {Rotation}";
}