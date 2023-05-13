// -----------------------------------------------------------------------
// <copyright file="CursedModConfigurationManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using CursedMod.Features.Logger;
using Serialization;

namespace CursedMod.Loader.Configurations;

public static class CursedModConfigurationManager
{
    public static CursedModConfiguration LoadedConfiguration;
    
    internal static void LoadConfigurations()
    {
        try
        {
            if (!CursedPaths.Configuration.Exists)
            {
                LoadedConfiguration = new CursedModConfiguration();
                File.WriteAllText(CursedPaths.Configuration.FullName, YamlParser.Serializer.Serialize(LoadedConfiguration));
                return;
            }
            
            LoadedConfiguration = YamlParser.Deserializer.Deserialize<CursedModConfiguration>(File.ReadAllText(CursedPaths.Configuration.FullName));
        }
        catch (Exception e)
        {
            CursedLogger.LogError("Couldn't load CursedMod's configuration file. Exception below:");
            CursedLogger.LogError(e);
        }
    }
}