using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Player.Dummies;
using CursedMod.Features.Wrappers.Server;
using PluginAPI.Core;
using UnityEngine.SceneManagement;

namespace CursedMod.Events.Handlers.MapGeneration;

public static class MapGenerationEventHandler
{
    public static void CacheAPI()
    {
        CursedLogger.InternalDebug("Caching api");
        CursedFacility.AmbientSoundPlayer = CursedServer.LocalPlayer.GetComponent<AmbientSoundPlayer>();
        CursedWarhead.OutsidePanel = CursedWarhead.Controller.GetComponent<AlphaWarheadOutsitePanel>();
    }

    public static void OnChangingScene(Scene scene, LoadSceneMode loadMode)
    {
        CursedLogger.InternalDebug("Loading scene " + scene.name + " with mode " + loadMode);
        if (scene.name != "Facility")
            return;
        
        CursedPlayer.Dictionary.Clear();
        CursedDummy.Dictionary.Clear();
    }
}