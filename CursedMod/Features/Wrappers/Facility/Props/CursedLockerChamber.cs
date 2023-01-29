// -----------------------------------------------------------------------
// <copyright file="CursedLockerChamber.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using CursedMod.Features.Wrappers.Inventory.Pickups;
using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedLockerChamber
{
    internal CursedLockerChamber(LockerChamber lockerChamber)
    {
        Base = lockerChamber;
        GameObject = Base.gameObject;
        Transform = Base.transform;
    }
    
    public LockerChamber Base { get; }
    
    public GameObject GameObject { get; }
   
    public Transform Transform { get; }

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
    
    public bool CanInteract => Base.CanInteract;
    
    public IEnumerable<CursedPickup> GetSpawnedItems() => Base._content.Select(CursedPickup.Get);
    
    public IEnumerable<CursedPickup> GetItemsToBeSpawned() => Base._toBeSpawned.Select(CursedPickup.Get);

    public void SpawnItem(ItemType itemType, int amount) => Base.SpawnItem(itemType, amount);

    public void SetDoor(bool status, AudioClip clip) => Base.SetDoor(status, clip);

    public void PlayDeniedSound(AudioClip clip) => Base.PlayDenied(clip);

    public override string ToString() => $"{nameof(CursedLockerChamber)}: Opened: {IsOpen} | CanInteract: {CanInteract} | Position: {Position} | Rotation: {Rotation}";
}