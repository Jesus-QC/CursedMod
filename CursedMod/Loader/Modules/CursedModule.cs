// -----------------------------------------------------------------------
// <copyright file="CursedModule.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using CursedMod.Features.Logger;
using CursedMod.Loader.Modules.Configuration;
using CursedMod.Loader.Modules.Enums;
using Serialization;

namespace CursedMod.Loader.Modules;

public abstract class CursedModule : ICursedModule
{
    public Assembly ModuleAssembly { get; set; }
    
    public virtual string ModuleName { get; set; } = "Unknown";

    public virtual string ModuleAuthor { get; set; } = "Unknown";

    public virtual string ModuleVersion { get; set; } = "0.0.0.0";

    public virtual byte LoadPriority { get; set; } = (byte)ModulePriority.Medium;
    
    public virtual string CursedModVersion { get; set; } = CursedModInformation.Version;

    public DirectoryInfo ModuleDirectory { get; set; }

    public CursedModuleProperties ModuleProperties { get; set; } = new ();

    public virtual void OnLoaded()
    {
        CursedLogger.LogInformation($"Enabled {this}");
    }

    public virtual void OnUnloaded()
    {
        CursedLogger.LogInformation($"Unloaded {this}");
    }

    public T GetConfig<T>(string name)
    {
        string configPath = Path.Combine(ModuleDirectory.FullName, name + ".yml");

        if (File.Exists(configPath))
        {
            T config = YamlParser.Deserializer.Deserialize<T>(File.ReadAllText(configPath));
            SaveConfig(config, name); // Updates new properties inside the config
            return config;
        }
        
        T newInstance = Activator.CreateInstance<T>();
        SaveConfig(newInstance, name);
        return newInstance;
    }

    public void SaveConfig<T>(T instance, string name)
    {
        string path = Path.Combine(ModuleDirectory.FullName, name + ".yml");
        File.WriteAllText(path, YamlParser.Serializer.Serialize(instance));
    }

    public override string ToString()
    {
        return $"'{ModuleName}', Version: {ModuleVersion}, Author: '{ModuleAuthor}'";
    }
}