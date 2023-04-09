// -----------------------------------------------------------------------
// <copyright file="CursedModule.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommandSystem;
using CursedMod.Features.Logger;
using CursedMod.Loader.Commands;
using CursedMod.Loader.Modules.Configuration;
using CursedMod.Loader.Modules.Enums;
using RemoteAdmin;
using Serialization;

namespace CursedMod.Loader.Modules;

public abstract class CursedModule : ICursedModule
{
    public Assembly ModuleAssembly { get; set; }
    
    public virtual string ModuleName => "Unknown";

    public virtual string ModuleAuthor => "Unknown";

    public virtual string ModuleVersion => "0.0.0.0";

    public virtual byte LoadPriority => (byte)ModulePriority.Medium;

    public virtual string CursedModVersion => CursedModInformation.Version;

    public DirectoryInfo ModuleDirectory { get; set; }

    public CursedModuleProperties ModuleProperties { get; set; } = new ();

    public ModuleType ModuleType { get; set; } = ModuleType.Global;

    public Dictionary<CursedCommandType, HashSet<ICommand>> RegisteredCommands { get; } = new (3) { [CursedCommandType.Client] = new HashSet<ICommand>(), [CursedCommandType.GameConsole] = new HashSet<ICommand>(), [CursedCommandType.RemoteAdmin] = new HashSet<ICommand>() };

    public virtual void OnLoaded()
    {
        CursedLogger.LogInformation($"Enabled {this}");
    }

    public virtual void OnUnloaded()
    {
        CursedLogger.LogInformation($"Unloaded {this}");
    }

    public void OnRegisteringCommands()
    {
        foreach (Type type in ModuleAssembly.GetTypes())
        {
            try
            {
                if (!type.IsClass || !typeof(ICommand).IsAssignableFrom(type)) 
                    continue;
                
                foreach (CustomAttributeData customAttributeData in type.GetCustomAttributesData())
                {
                    if (customAttributeData.AttributeType != typeof(CommandHandlerAttribute)) 
                        continue;
                    
                    Type cmdType = (Type)customAttributeData.ConstructorArguments[0].Value;

                    CursedCommandType commandType;
                    if (cmdType == typeof(GameConsoleCommandHandler))
                        commandType = CursedCommandType.GameConsole;
                    else if (cmdType == typeof(RemoteAdminCommandHandler))
                        commandType = CursedCommandType.RemoteAdmin;
                    else if (cmdType == typeof(ClientCommandHandler))
                        commandType = CursedCommandType.Client;
                    else
                        continue;

                    ICommand command = CursedCommandManager.RegisterCommand(commandType, type);
                    if (command is null)
                        continue;

                    RegisteredCommands[commandType].Add(command);
                }
            }
            catch (Exception e)
            {
                CursedLogger.LogError("There was an issue while registering a command, exception below:");
                CursedLogger.LogError(e);
            }
        }
    }

    public void OnUnregisteringCommands()
    {
        foreach (ICommand command in RegisteredCommands[CursedCommandType.GameConsole])
        {
            GameCore.Console.singleton.ConsoleCommandHandler.UnregisterCommand(command);
        }
        
        RegisteredCommands[CursedCommandType.GameConsole].Clear();
        
        foreach (ICommand command in RegisteredCommands[CursedCommandType.RemoteAdmin])
        {
            CommandProcessor.RemoteAdminCommandHandler.UnregisterCommand(command);
        }
        
        RegisteredCommands[CursedCommandType.RemoteAdmin].Clear();
        
        foreach (ICommand command in RegisteredCommands[CursedCommandType.Client])
        {
            QueryProcessor.DotCommandHandler.UnregisterCommand(command);
        }
        
        RegisteredCommands[CursedCommandType.Client].Clear();
    }

    public T GetConfig<T>(string name)
    {
        try
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
        catch (Exception e)
        {
            CursedLogger.LogError("There was an issue while loading a config, exception below:");
            CursedLogger.LogError(e);
        }

        return Activator.CreateInstance<T>();
    }

    public void SaveConfig<T>(T instance, string name)
    {
        try
        {
            string path = Path.Combine(ModuleDirectory.FullName, name + ".yml");
            File.WriteAllText(path, YamlParser.Serializer.Serialize(instance));
        }
        catch (Exception e)
        {
            CursedLogger.LogError("There was an issue while saving a config, exception below:");
            CursedLogger.LogError(e);
        }
    }

    public override string ToString()
    {
        return $"'{ModuleName}', Version: {ModuleVersion}, Author: '{ModuleAuthor}'";
    }
}