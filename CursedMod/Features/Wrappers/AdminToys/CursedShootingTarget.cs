// -----------------------------------------------------------------------
// <copyright file="CursedShootingTarget.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AdminToys;
using CursedMod.Features.Enums;
using PlayerStatsSystem;
using UnityEngine;

namespace CursedMod.Features.Wrappers.AdminToys;

public class CursedShootingTarget : CursedAdminToy
{
    internal CursedShootingTarget(ShootingTarget toyBase)
        : base(toyBase)
    {
        Base = toyBase;
        TargetType = GameObject.name switch
        {
            "sportTargetPrefab" => ShootingTargetType.Sport,
            "dboyTargetPrefab" => ShootingTargetType.DBoy,
            "binaryTargetPrefab" => ShootingTargetType.Binary,
            _ => ShootingTargetType.Other
        };
    }
    
    public ShootingTarget Base { get; }

    public ShootingTargetType TargetType { get; }

    public bool ServerSynced
    {
        get => Base._syncMode;
        set => Base.Network_syncMode = value;
    }

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

    public static CursedShootingTarget Create(ShootingTargetType type, Vector3? position = null, Vector3? scale = null, Vector3? rotation = null,bool spawn = false)
    {
        ShootingTarget target = type switch
        {
            ShootingTargetType.Binary => CursedPrefabManager.BinaryShootingTarget,
            ShootingTargetType.DBoy => CursedPrefabManager.DBoyShootingTarget,
            ShootingTargetType.Sport => CursedPrefabManager.SportShootingTarget,
            _ => CursedPrefabManager.DBoyShootingTarget,
        };

        ShootingTarget shootingTarget = Object.Instantiate(target);
        CursedShootingTarget cursedTarget = new (shootingTarget);
        
        if (position.HasValue)
            cursedTarget.Position = position.Value;

        if (scale.HasValue)
            cursedTarget.Scale = scale.Value;

        if (rotation.HasValue)
            cursedTarget.Rotation = rotation.Value;

        if (spawn)
            cursedTarget.Spawn();

        return cursedTarget;
    }
    
    public bool Damage(float damage, DamageHandlerBase damageHandler, Vector3 exactHit) => Base.Damage(damage, damageHandler, exactHit);

    public void SendInfo(int maxHp, int autoReset) => Base.RpcSendInfo(maxHp, autoReset);
}