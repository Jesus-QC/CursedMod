// -----------------------------------------------------------------------
// <copyright file="ICursedModule.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommandSystem;
using CursedMod.Loader.Commands;
using CursedMod.Loader.Modules.Configuration;
using CursedMod.Loader.Modules.Enums;

namespace CursedMod.Loader.Modules;

public interface ICursedModule
{
    public Assembly ModuleAssembly { get; set; }
    
    public string ModuleName { get; }
    
    public string ModuleAuthor { get; }
    
    public string ModuleVersion { get; }
    
    public string CursedModVersion { get; }
    
    public byte LoadPriority { get; }
    
    public DirectoryInfo ModuleDirectory { get; set; }
    
    public CursedModuleProperties ModuleProperties { get; set; }
    
    public ModuleType ModuleType { get; set; }

    public Dictionary<CursedCommandType, HashSet<ICommand>> RegisteredCommands { get; }

    void OnLoaded();
    
    void OnUnloaded();

    void OnRegisteringCommands();

    void OnUnregisteringCommands();
    
    T GetConfig<T>(string name);
    
    void SaveConfig<T>(T instance, string name);
}