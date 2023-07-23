// -----------------------------------------------------------------------
// <copyright file="Cursed079Camera.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class Cursed079Camera
{
    public static readonly Dictionary<Scp079Camera, Cursed079Camera> Dictionary = new ();
    
    internal Cursed079Camera(Scp079Camera baseCamera)
    {
        BaseCamera = baseCamera;
        
        Dictionary.Add(baseCamera, this);
    }
    
    public static IEnumerable<Cursed079Camera> Collection => Dictionary.Values;
    
    public static IEnumerable<Cursed079Camera> List => Dictionary.Values.ToList();
    
    public Scp079Camera BaseCamera { get; }
    
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
    
    public static Cursed079Camera Get(Scp079Camera camera) => Dictionary.ContainsKey(camera) ? Dictionary[camera] : new Cursed079Camera(camera);

    public override string ToString() => $"{nameof(Cursed079Camera)}: Name: {CameraName} | Position: {Position} | Rotation: {Rotation} | Zoom: {CurrentZoom} | Zone: {Zone} | Id: {CameraId}";

    internal static void CacheAllCameras()
    {
        foreach (Scp079InteractableBase interactable in Scp079InteractableBase.AllInstances)
        {
            if (interactable is not Scp079Camera camera)
                continue;

            Get(camera);
        }
    }
}
