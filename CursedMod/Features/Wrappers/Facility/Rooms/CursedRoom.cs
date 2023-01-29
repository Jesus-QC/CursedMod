// -----------------------------------------------------------------------
// <copyright file="CursedRoom.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using MapGeneration;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class CursedRoom
{
    internal CursedRoom(RoomIdentifier room)
    {
        Room = room;
    }
    
    public RoomIdentifier Room { get; }

    public Vector3 Position => Room.transform.position;
    
    public Quaternion Rotation => Room.transform.rotation;
    
    public GameObject GameObject => Room.gameObject;
    
    public Transform Transform => Room.transform;
    
    public RoomName Name => Room.Name;
    
    public FacilityZone Zone => Room.Zone;
}