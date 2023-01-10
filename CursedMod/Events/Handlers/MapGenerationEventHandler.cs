using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Server;

namespace CursedMod.Events.Handlers;

public static class MapGenerationEventHandler
{
    // TODO, adding this because needed in the API
    private static void CacheAPI()
    {
        CursedFacility.AmbientSoundPlayer = CursedServer.LocalPlayer.GetComponent<AmbientSoundPlayer>();
        CursedWarhead.OutsidePanel = CursedWarhead.Controller.GetComponent<AlphaWarheadOutsitePanel>();
    }
}