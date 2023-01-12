using MapGeneration;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public static class CursedRoom
{
    public static RoomIdentifier Room { get; }

    public static Vector3 Position => Room.transform.position;
    
    public static Quaternion Rotation => Room.transform.rotation;
    
    public static RoomName Name => Room.Name;
    
    public static FacilityZone Zone => Room.Zone;
}