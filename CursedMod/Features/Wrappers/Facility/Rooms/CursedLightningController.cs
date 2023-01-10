using MapGeneration;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Rooms;

public class CursedLightningController
{
    public FlickerableLightController Base { get; }

    public RoomIdentifier Room => Base.Room; // todo: move to CursedRoom

    public Color Color
    {
        get => Base._warheadLightColor;
        set
        {
            Base.Network_warheadLightColor = value;
            WarheadLightOverride = true;
        }
    }

    public bool WarheadLightOverride
    {
        get => Base._warheadLightOverride;
        set => Base.Network_warheadLightOverride = value;
    }

    public bool LightsEnabled
    {
        get => Base.LightsEnabled;
        set => Base.NetworkLightsEnabled = value;
    }

    public float LightIntensityMultiplier
    {
        get => Base._lightIntensityMultiplier;
        set => Base.Network_lightIntensityMultiplier = value;
    }

    public void ResetColor()
    {
        Color = FlickerableLightController.DefaultWarheadColor;
        WarheadLightOverride = false;
    }
    
    public CursedLightningController(FlickerableLightController controller)
    {
        Base = controller;
    }
}