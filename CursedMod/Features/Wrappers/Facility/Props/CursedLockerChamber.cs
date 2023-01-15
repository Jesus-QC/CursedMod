using MapGeneration.Distributors;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedLockerChamber
{
    public LockerChamber Base { get; }
    
    public CursedLockerChamber(LockerChamber locker)
    {
        Base = locker;
    }

    public bool IsOpen
    {
        get => Base.IsOpen;
        set => Base.IsOpen = value;
    }

    public bool CanInteract => Base.CanInteract;

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

    public Transform Transform => Base.transform;

    public void SetDoor(bool status, AudioClip clip) => Base.SetDoor(status, clip);

    public void PlayDeniedSound(AudioClip clip) => Base.PlayDenied(clip);

    public override string ToString() => $"{nameof(CursedLockerChamber)}: Opened: {IsOpen} | CanInteract: {CanInteract} | Position: {Position} | Rotation: {Rotation}";
}