using CursedMod.Events;
using CursedMod.Loader;
using CursedMod.Loader.Configurations;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace CursedMod;

internal class EntryPoint
{
    [PluginEntryPoint("CursedMod", CursedModInformation.Version, "A rich low level modding framework.", "Jesus-QC")]
    private void Init()
    {
        if (!ModConfiguration.LoadCursedMod)
            return;

        Log.Info("CursedMod is being loaded");
        
        EventManager.PatchEvents();
        
        // Patch Events
        // Load Plugins
    }

    [PluginConfig]
    public static CursedModConfiguration ModConfiguration;
}
