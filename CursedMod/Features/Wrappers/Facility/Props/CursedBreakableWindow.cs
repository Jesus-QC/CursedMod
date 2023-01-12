using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedBreakableWindow
{
    public BreakableWindow Base { get; }

    public Vector3 Position
    {
        get => Base._transform.position;
        set => Base._transform.position = value;
    }

    public Quaternion Rotation
    {
        get => Base._transform.rotation;
        set => Base._transform.rotation = value;
    }

    public bool IsBroken
    {
        get => Base.syncStatus.broken;
        set => Base.syncStatus.broken = value;
    }

    public void Break() => Base.isBroken = true;

    public bool Damage(float damage, DamageHandlerBase damageHandlerBase, Vector3 position) => Base.Damage(damage, damageHandlerBase, position);

    public void ForceDamage(float damage) => Base.ServerDamageWindow(damage);
    
    public CursedBreakableWindow(BreakableWindow window)
    {
        Base = window;
    }
}