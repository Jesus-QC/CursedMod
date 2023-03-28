// -----------------------------------------------------------------------
// <copyright file="CursedLoader.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CursedMod.Features.Logger;
using CursedMod.Loader.Modules;
using CursedMod.Loader.Modules.Configuration;
using CursedMod.Loader.Modules.Enums;
using Serialization;

namespace CursedMod.Loader;

public static class CursedLoader
{
    public static SortedSet<ICursedModule> LoadedModules { get; } = new (new CursedModulePriorityComparer());
    
    public static HashSet<ICursedModule> EnabledModules { get; } = new ();

    public static void LoadAll()
    {
        LoadAllDependencies();
        LoadAllModules();
        EnableAllModules();
    }
    
    public static void LoadAllDependencies()
    {
        CursedLogger.InternalPrint("Loading global dependencies.");
        LoadDependencies(CursedPaths.GlobalDependenciesPath.GetFiles("*.dll"));
        CursedLogger.InternalPrint("Loading local dependencies.");
        LoadDependencies(CursedPaths.LocalDependenciesPath.GetFiles("*.dll"));
    }

    public static void LoadDependencies(IEnumerable<FileInfo> files)
    {
        foreach (FileInfo file in files)
        {
            try
            {
                string dependencyName = Assembly.Load(File.ReadAllBytes(file.FullName)).FullName;
                CursedLogger.LogInformation($"Loaded {dependencyName}");
            }
            catch (Exception e)
            {
                CursedLogger.LogError($"Couldn't load the dependency inside '{file.FullName}'");
                CursedLogger.LogError(e);
            }
        }
    }
    
    public static void LoadAllModules()
    {
        CursedLogger.InternalPrint("Loading global modules.");
        LoadModules(CursedPaths.GlobalPluginsPath.GetFiles("*.dll"), ModuleType.Global);
        CursedLogger.InternalPrint("Loading local modules.");
        LoadModules(CursedPaths.LocalPluginsPath.GetFiles("*.dll"), ModuleType.Local);
    }

    public static void LoadModules(IEnumerable<FileInfo> files, ModuleType moduleType)
    {
        foreach (FileInfo file in files)
        {
            try
            {
                Assembly moduleAssembly = Assembly.Load(File.ReadAllBytes(file.FullName));

                foreach (Type type in moduleAssembly.GetTypes())
                {
                    if (!type.IsSubclassOf(typeof(CursedModule))) 
                        continue;
                    
                    ICursedModule module = Activator.CreateInstance(type) as ICursedModule;
                    LoadModule(module, moduleAssembly, moduleType);
                }
            }
            catch (Exception e)
            {
                CursedLogger.LogError($"Couldn't load the module inside '{file.FullName}'");
                CursedLogger.LogError(e);
            }
        }
    }

    public static void EnableAllModules()
    {
        CursedLogger.InternalPrint("Enabling modules.");
        
        foreach (ICursedModule module in LoadedModules)
        {
            EnableModule(module);
            EnabledModules.Add(module);
        }
    }

    public static void DisableModules()
    {
        CursedLogger.InternalPrint("Disabling modules.");
        
        foreach (ICursedModule module in EnabledModules)
        {
            DisableModule(module);
        }
        
        EnabledModules.Clear();
    }

    public static void LoadModule(ICursedModule module, Assembly assembly, ModuleType moduleType)
    {
        CursedLogger.LogInformation($"Loading {module}");
        module.ModuleAssembly = assembly;
        LoadModuleProperties(module, moduleType);
        LoadedModules.Add(module);
    }

    public static void EnableModule(ICursedModule module)
    {
        try
        {
            if (!module.ModuleProperties.IsEnabled)
                return;
            
            module.OnLoaded();
            module.OnRegisteringCommands();
        }
        catch (Exception e)
        {
            CursedLogger.LogError($"Couldn't load the module {module}. Exception below:");
            CursedLogger.LogError(e);
        }
    }

    public static void DisableModule(ICursedModule module)
    {
        try
        {
            module.OnUnloaded();
            module.OnUnregisteringCommands();
        }
        catch (Exception e)
        {
            CursedLogger.LogError($"Couldn't load the module {module}. Exception below:");
            CursedLogger.LogError(e);
        }
    }

    public static void LoadModuleProperties(ICursedModule module, ModuleType moduleType)
    {
        try
        {
            module.ModuleType = moduleType;

            DirectoryInfo pluginsDirectory = moduleType == ModuleType.Global ? CursedPaths.GlobalPluginsPath : CursedPaths.LocalPluginsPath;
            module.ModuleDirectory = Directory.CreateDirectory(Path.Combine(pluginsDirectory.FullName, module.ModuleName));
        
            string propertiesPath = Path.Combine(module.ModuleDirectory.FullName, "properties.yml");
        
            if (!File.Exists(propertiesPath))
            {
                CursedLogger.InternalDebug($"Creating {module} properties.");
                File.WriteAllText(propertiesPath, YamlParser.Serializer.Serialize(module.ModuleProperties));
                return;
            }

            CursedLogger.InternalDebug($"Loading {module} properties.");
            module.ModuleProperties = YamlParser.Deserializer.Deserialize<CursedModuleProperties>(File.ReadAllText(propertiesPath));
        }
        catch (Exception e)
        {
            CursedLogger.LogError($"Couldn't load the properties of the module {module}. Exception below:");
            CursedLogger.LogError(e);
        }
    }
}