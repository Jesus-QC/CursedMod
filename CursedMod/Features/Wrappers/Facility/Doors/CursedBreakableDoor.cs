using CursedMod.Features.Enums;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;

namespace CursedMod.Features.Wrappers.Facility.Doors;

public class CursedBreakableDoor : CursedDoor
{
    public BreakableDoor BreakableBase { get; }
    
    internal CursedBreakableDoor(BreakableDoor door) : base(door)
    {
        BreakableBase = door;
        DoorType = DoorType.Breakable;
    }

    public float GetHealthPercent() => BreakableBase.GetHealthPercent();

    public float RemainingHealth
    {
        get => BreakableBase.RemainingHealth;
        set => BreakableBase.RemainingHealth = value;
    }

    public float MaxHealth
    {
        get => BreakableBase.MaxHealth;
        set => BreakableBase.MaxHealth = value;
    }

    public DoorDamageType IgnoredDamageSources
    {
        get => BreakableBase.IgnoredDamageSources;
        set => BreakableBase.IgnoredDamageSources = value;
    }

    public bool IsDestroyed
    {
        get => BreakableBase.IsDestroyed;
        set => BreakableBase.IsDestroyed = value;
    }

    public bool IgnoreLockdowns => BreakableBase.IgnoreLockdowns;

    public bool IgnoreRemoteAdmin => BreakableBase.IgnoreRemoteAdmin;
    
    public bool IsScp106Passable => BreakableBase.IsScp106Passable;

    public bool Damage(float hp, DoorDamageType type) => BreakableBase.ServerDamage(hp, type);
    
    public bool Destroy(DoorDamageType type = DoorDamageType.ServerCommand)
    {
        if (BreakableBase.IsDestroyed) 
            return false;
        
        BreakableBase.ServerDamage(MaxHealth, type);
        return true;
    }
}