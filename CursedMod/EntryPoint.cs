// -----------------------------------------------------------------------
// <copyright file="EntryPoint.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events;
using CursedMod.Features.Logger;
using CursedMod.Loader;
using CursedMod.Loader.Configurations;
using PluginAPI.Core.Attributes;

namespace CursedMod;

internal class EntryPoint
{
    [PluginEntryPoint("CursedMod", CursedModInformation.Version, "A rich low level modding framework.", "Jesus-QC")]
    private void Init()
    {
        CursedPaths.LoadPaths();
        CursedModConfigurationManager.LoadConfigurations();

        if (!CursedModConfigurationManager.LoadedConfiguration.LoadCursedMod)
        {
            CursedLogger.InternalPrint("CursedMod is disabled inside the configuration.");
            return;
        }
        
        CursedLogger.InternalPrint($"Welcome to CursedMod {CursedModInformation.Version}");
        
        CursedLoader.LoadAll();
        
        CursedEventManager.PatchEvents();
    }
}
