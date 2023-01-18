using System.Collections.Generic;
using System.Linq;
using CursedMod.Features.Enums;
using CursedMod.Features.Wrappers.Player;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;

namespace CursedMod.Features.Wrappers.Facility.Doors;

public class CursedCheckpointDoor : CursedDoor
{
    public CheckpointDoor CheckpointBase { get; }
    
    internal CursedCheckpointDoor(CheckpointDoor door) : base(door)
    {
        CheckpointBase = door;
        DoorType = DoorType.Checkpoint;
    }

    public IEnumerable<CursedDoor> GetSubDoors() => CheckpointBase._subDoors.Select(Get);

    public bool IsDestroyed => CheckpointBase.IsDestroyed;

    public void ToggleAllDoors(bool newState) => CheckpointBase.ToggleAllDoors(newState);

    public bool DamageDoors(float hp, DoorDamageType type) => CheckpointBase.ServerDamage(hp, type);

    public float GetHealthPercent() => CheckpointBase.GetHealthPercent();

    public float OpeningTime
    {
        get => CheckpointBase._openingTime;
        set => CheckpointBase._openingTime = value;
    }

    public float WaitTime
    {
        get => CheckpointBase._waitTime;
        set => CheckpointBase._waitTime = value;
    }

    public float WarningTime
    {
        get => CheckpointBase._warningTime;
        set => CheckpointBase._warningTime = value;
    }

    public bool PermanentlyDestroyed
    {
        get => CheckpointBase._permanentDestroyment;
        set => CheckpointBase._permanentDestroyment = value;
    }

    public CheckpointDoor.CheckpointSequenceStage CurrentSequence
    {
        get => CheckpointBase._currentSequence;
        set => CheckpointBase._currentSequence = value;
    }
}