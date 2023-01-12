using PlayerRoles.PlayableScps.Scp079.Cameras;
using MapGeneration;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public class CursedCamera
{
    public Scp079Camera BaseCamera { get; }
    
    public CursedCamera(Scp079Camera baseCamera)
    {
        BaseCamera = baseCamera;
    }

    public bool IsActive
    {
        get => BaseCamera.IsActive;
        set => BaseCamera.IsActive = value;
    }
    public GameObject GameObject => BaseCamera.gameObject;
    
    public Transform Transform => BaseCamera.transform;
    
    public string CameraName => BaseCamera.Label;
    
    public Vector3 Position => BaseCamera.Position;
    
    public Quaternion Rotation
    {
        get => BaseCamera._cameraAnchor.rotation;
        set => BaseCamera._cameraAnchor.rotation = value;
    }

    public Vector3 Scale
    {
        get => BaseCamera._cameraAnchor.localScale;
        set => BaseCamera._cameraAnchor.localScale = value;
    }

    public float CurrentZoom => BaseCamera.ZoomAxis.CurrentZoom;
    
    public bool IsUsing
    {
        get => BaseCamera.IsActive;
        set => BaseCamera.IsActive = value;
    }

    public FacilityZone Zone => BaseCamera.Room.Zone;

    public ushort CameraId => BaseCamera.SyncId;

    public override string ToString() => $"{nameof(CursedCamera)}: Name: {CameraName} | Position: {Position} | Rotation: {Rotation} | Zoom: {CurrentZoom} | Zone: {Zone} | Id: {CameraId}";
}