using System;
using AdminToys;
using CursedMod.Features.Enums;
using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.AdminToys;

public class CursedShootingTarget : CursedAdminToy
{
    public ShootingTarget Base { get; }
    
    internal CursedShootingTarget(ShootingTarget toyBase) : base(toyBase)
    {
        Base = toyBase;
        TargetType = GameObject.name switch
        {
            "sportTargetPrefab" => ShootingTargetType.Sport,
            "dboyTargetPrefab" => ShootingTargetType.DBoy,
            "binaryTargetPrefab" => ShootingTargetType.Binary,
            _  => ShootingTargetType.Other
        };
    }
    
    public readonly ShootingTargetType TargetType;

    public bool ServerSynced
    {
        get => Base._syncMode;
        set => Base.Network_syncMode = value;
    }

    public bool Damage(float damage, DamageHandlerBase damageHandler, Vector3 exactHit) =>
        Base.Damage(damage, damageHandler, exactHit);

    public void SendInfo(int maxHp, int autoReset) => Base.RpcSendInfo(maxHp, autoReset);

    public float Health
    {
        get => Base._hp;
        set => Base._hp = value;
    }
    
    public int MaxHealth
    {
        get => Base._maxHp;
        set
        {
            Base._maxHp = value;
            SendInfo(value, AutoResetTime);
        }
    }
    
    public int AutoResetTime
    {
        get => Base._autoDestroyTime;
        set
        {
            Base._autoDestroyTime = value;
            SendInfo(MaxHealth, AutoResetTime);
        }
    }
}