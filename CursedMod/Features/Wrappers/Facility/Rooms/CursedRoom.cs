using MapGeneration;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class CursedRoom
{
    public RoomIdentifier Room { get; }
    
    public CursedRoom(RoomIdentifier room)
    {
        Room = room;
    }

    public Vector3 Position => Room.transform.position;
    
    public Quaternion Rotation => Room.transform.rotation;
    
    public GameObject GameObject => Room.gameObject;
    
    public Transform Transform => Room.transform;
    
    public RoomName Name => Room.Name;
    
    public FacilityZone Zone => Room.Zone;
}