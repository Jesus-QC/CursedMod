using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Elevators;

public class CursedElevatorChamber
{
    public ElevatorChamber Base { get; }

    public CursedElevatorChamber(ElevatorChamber chamber)
    {
        Base = chamber;
    }

    public ElevatorDoor CurrentDestination
    {
        get => Base.CurrentDestination;
        set => Base.CurrentDestination = value;
    }

    public int CurrentLevel
    {
        get => Base.CurrentLevel;
        set => Base.CurrentLevel = value;
    }

    public ElevatorManager.ElevatorGroup AssignedGroup
    {
        get => Base.AssignedGroup;
        set => Base.AssignedGroup = value;
    }

    public DoorLockReason ActiveLocks
    {
        get => Base.ActiveLocks;
        set => Base.ActiveLocks = value;
    }

    public bool IsReady => Base.IsReady;

    public Bounds WorldSpaceBounds => Base.WorldspaceBounds;

    public bool TrySetDestination(int targetLevel, bool force = false) => Base.TrySetDestination(targetLevel, force);

    public void SetInnerDoor(bool state) => Base.SetInnerDoor(state);

    public void AddNewPanel(ElevatorManager.ElevatorGroup group, ElevatorDoor door) => Base.AddNewPanel(group, door);
}