// -----------------------------------------------------------------------
// <copyright file="CursedPickup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Inventory.Pickups.Firearms;
using CursedMod.Features.Wrappers.Inventory.Pickups.Firearms.Ammo;
using CursedMod.Features.Wrappers.Inventory.Pickups.MicroHID;
using CursedMod.Features.Wrappers.Inventory.Pickups.Radio;
using CursedMod.Features.Wrappers.Inventory.Pickups.ThrowableProjectiles;
using CursedMod.Features.Wrappers.Inventory.Pickups.Usables;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Server;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.MicroHID;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Radio;
using InventorySystem.Items.ThrowableProjectiles;
using InventorySystem.Items.Usables.Scp1576;
using InventorySystem.Items.Usables.Scp244;
using InventorySystem.Items.Usables.Scp330;
using Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Inventory.Pickups;

public class CursedPickup
{
    internal CursedPickup(ItemPickupBase itemPickupBase)
    {
        Base = itemPickupBase;
        GameObject = Base.gameObject;
        Transform = Base._transform;
        Rigidbody = Base.RigidBody;
    }
    
    public ItemPickupBase Base { get; }
    
    public GameObject GameObject { get; }
    
    public Transform Transform { get; }
    
    public Rigidbody Rigidbody { get; }

    public PickupSyncInfo Info
    {
        get => Base.Info;
        set => Base.NetworkInfo = value;
    }
    
    public ushort Serial => Info.Serial;

    public ItemType ItemType
    {
        get => Info.ItemId;
        set => Info = new PickupSyncInfo(value, Info.Position, Info.Rotation, Info.Weight, Info.Serial);
    }
    
    public CursedPlayer PreviousOwner => CursedPlayer.Get(Base.PreviousOwner.Hub);

    public Vector3 Position
    {
        get => Info.Position;
        set
        {
            Transform.position = value;
            Info = new PickupSyncInfo(Info.ItemId, value, Info.Rotation, Info.Weight, Info.Serial);
        }
    }

    public Quaternion Rotation
    {
        get => Info.Rotation;
        set
        {
            Transform.rotation = value;
            Info = new PickupSyncInfo(Info.ItemId, Info.Position, value, Info.Weight, Info.Serial);
        }
    }

    public Vector3 Scale
    {
        get => Transform.localScale;
        set
        {
            Transform.localScale = value;
            CursedPlayer.SendSpawnMessageToAll(Base.netIdentity);
        }
    }

    public float Weight
    {
        get => Base.Info.Weight;
        set => Info = new PickupSyncInfo(Info.ItemId, Info.Position, Info.Rotation, value, Info.Serial);
    }

    public bool IsLocked
    {
        get => Base.Info.Locked;
        set
        {
            PickupSyncInfo info = Info;
            info.Locked = value;
            Info = info;
        }
    }

    public static CursedPickup Get(ItemPickupBase pickupBase)
    {
        return pickupBase switch
        {
            AmmoPickup ammoPickup => new CursedAmmoPickup(ammoPickup),
            FirearmPickup firearmPickup => new CursedFirearmPickup(firearmPickup),
            MicroHIDPickup microHidPickup => new CursedMicroHidPickup(microHidPickup),
            RadioPickup radioPickup => new CursedRadioPickup(radioPickup),
            TimedGrenadePickup timedGrenadePickup => new CursedTimedGrenadePickup(timedGrenadePickup),
            Scp244DeployablePickup scp244DeployablePickup => new CursedScp244Pickup(scp244DeployablePickup),
            Scp330Pickup scp330Pickup => new CursedScp330Pickup(scp330Pickup),
            Scp1576Pickup scp1576Pickup => new CursedScp1576Pickup(scp1576Pickup),
            _ => new CursedPickup(pickupBase)
        };
    }

    public static CursedPickup Create(ItemType type, Vector3? position = null, Vector3? rotation = null, bool spawn = true)
    {
        ItemBase itemBase = CursedServer.LocalPlayer.AddItemBase(type);

        PickupSyncInfo pickupSyncInfo = new ()
        {
            ItemId = type,
            Weight = itemBase.Weight,
            Serial = ItemSerialGenerator.GenerateNext(),
        };

        ItemPickupBase pickupBase = CursedServer.LocalPlayer.Inventory.ServerCreatePickup(itemBase, pickupSyncInfo, spawn);

        if (position.HasValue)
            pickupBase.transform.position = position.Value;
        if (rotation.HasValue)
            pickupBase.transform.eulerAngles = rotation.Value;
        
        pickupBase.RefreshPositionAndRotation();

        return Get(pickupBase);
    }
    
    public GameObject Spawn()
    {
        NetworkServer.Spawn(GameObject);
        return GameObject;
    }

    public void UnSpawn()
    {
        NetworkServer.UnSpawn(GameObject);
    }
    
    public void Destroy() => Base.DestroySelf();
}