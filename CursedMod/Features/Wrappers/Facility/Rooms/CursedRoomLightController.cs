// -----------------------------------------------------------------------
// <copyright file="CursedRoomLightController.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class CursedRoomLightController
{
    public static readonly Dictionary<RoomLightController, CursedRoomLightController> Dictionary = new ();
    
    internal CursedRoomLightController(RoomLightController controller)
    {
        Base = controller;
        Dictionary.Add(controller, this);
    }
    
    public RoomLightController Base { get; }

    public Color Color
    {
        get => Base.NetworkOverrideColor;
        set
        {
            Base.NetworkOverrideColor = value;
            WarheadLightOverride = true;
        }
    }

    public bool WarheadLightOverride
    {
        get => Base.NetworkLightsEnabled;
        set => Base.NetworkLightsEnabled = value;
    }

    public CursedRoom Room => CursedRoom.Get(Base.Room);
    
    public static CursedRoomLightController Get(RoomLightController controller) => Dictionary.TryGetValue(controller, out CursedRoomLightController cursedController) ? cursedController : new CursedRoomLightController(controller);

    public void ResetColor()
    {
        WarheadLightOverride = false;
        Base.OverrideColorHook(Base.NetworkOverrideColor, default);
    }

    public void FlickerLights(float duration)
    {
        Base.ServerFlickerLights(duration);
    }
}
