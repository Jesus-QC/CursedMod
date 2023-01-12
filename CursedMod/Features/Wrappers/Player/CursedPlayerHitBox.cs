using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player;

public class CursedPlayerHitBox
{
    public HitboxIdentity Base { get; }

    public CursedPlayer Owner => CursedPlayer.Get(Base.TargetHub);

    public HitboxType HitBoxType
    {
        get => Base.HitboxType;
        set => Base._dmgMultiplier = value;
    }

    public bool Damage(float damage, DamageHandlerBase handler, Vector3 exactPosition) => Base.Damage(damage, handler, exactPosition);

    public void DisableColliders() => Base.SetColliders(false);
    
    public void EnableColliders() => Base.SetColliders(true);
}