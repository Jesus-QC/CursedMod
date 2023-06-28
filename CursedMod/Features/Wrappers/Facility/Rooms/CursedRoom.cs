// -----------------------------------------------------------------------
// <copyright file="CursedRoom.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using CursedMod.Features.Wrappers.Facility.Doors;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using UnityEngine;
using FacilityZone = MapGeneration.FacilityZone;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class CursedRoom
{
    public static readonly Dictionary<RoomIdentifier, CursedRoom> Dictionary = new ();

    private CursedRoom(RoomIdentifier room)
    {
        Room = room;

        Dictionary.Add(room, this);
        
        RoomLightController lightController = room.GetComponentInChildren<RoomLightController>();
        
        if (lightController is null)
            return;
        
        LightningController = new CursedLightningController(lightController);
    }
    
    public static IEnumerable<CursedRoom> Collection => Dictionary.Values;
    
    public static IEnumerable<CursedRoom> List => Dictionary.Values.ToList();
    
    public RoomIdentifier Room { get; }

    public CursedLightningController LightningController { get; }
    
    public Vector3 Position => Room.transform.position;
    
    public Quaternion Rotation => Room.transform.rotation;
    
    public GameObject GameObject => Room.gameObject;
    
    public Transform Transform => Room.transform;
    
    public RoomName Name => Room.Name;
    
    public FacilityZone Zone => Room.Zone;

    public static CursedRoom Get(RoomIdentifier roomIdentifier) => Dictionary.ContainsKey(roomIdentifier) ? Dictionary[roomIdentifier] : new CursedRoom(roomIdentifier);

    public static bool TryGet(RoomName roomName, out CursedRoom room)
    {
        foreach (CursedRoom r in Collection)
        {
            if (r.Name != roomName)
                continue;

            room = r;
            return true;
        }

        room = null;
        return false;
    }
    
    public static CursedRoom Get(RoomName roomName) => TryGet(roomName, out CursedRoom room) ? room : null;
    
    public IEnumerable<CursedDoor> GetDoors() => DoorVariant.DoorsByRoom.ContainsKey(Room) ? DoorVariant.DoorsByRoom[Room].Select(CursedDoor.Get) : Enumerable.Empty<CursedDoor>();

    public Vector3 GetGlobalPoint(Vector3 localPointOfRoom) => Transform.TransformPoint(localPointOfRoom);

    public Vector3 GetLocalPoint(Vector3 globalPoint) => Transform.InverseTransformPoint(globalPoint);
    
    internal static void CacheAllRooms()
    {
        foreach (RoomIdentifier room in RoomIdentifier.AllRoomIdentifiers)
        {
            Get(room);
        }
    }
}
