using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Server;
using PluginAPI.Core;

namespace CursedMod.Events.Handlers.MapGeneration;

public static class MapGenerationEventHandler
{
    // TODO, adding this because needed in the API
    public static void CacheAPI()
    {
        Log.Debug(nameof(CacheAPI), EntryPoint.ModConfiguration.ShowDebug);
        CursedFacility.AmbientSoundPlayer = CursedServer.LocalPlayer.GetComponent<AmbientSoundPlayer>();
        CursedWarhead.OutsidePanel = CursedWarhead.Controller.GetComponent<AlphaWarheadOutsitePanel>();
    }
}