// -----------------------------------------------------------------------
// <copyright file="CursedPaths.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using CursedMod.Features.Wrappers.Server;
using PluginAPI.Helpers;

namespace CursedMod.Loader;

public static class CursedPaths
{
    public static DirectoryInfo Main { get; private set; }
    
    public static DirectoryInfo Global { get; private set; }
    
    public static DirectoryInfo Local { get; private set; }
    
    public static DirectoryInfo GlobalPlugins { get; private set; }
    
    public static DirectoryInfo GlobalDependencies { get; private set; }
    
    public static DirectoryInfo LocalPlugins { get; private set; }

    public static DirectoryInfo LocalDependencies { get; private set; }
    
    public static FileInfo Permissions { get; private set; }

    public static void LoadPaths()
    {
        Main = Directory.CreateDirectory(Path.Combine(Paths.PluginAPI, "CursedMod"));
        Global = Main.CreateSubdirectory("Global");
        Local = Main.CreateSubdirectory(CursedServer.Port.ToString());

        GlobalPlugins = Global.CreateSubdirectory("Plugins");
        GlobalDependencies = Global.CreateSubdirectory("Dependencies");
        
        LocalPlugins = Local.CreateSubdirectory("Plugins");
        LocalDependencies = Local.CreateSubdirectory("Dependencies");

        Permissions = new FileInfo(Path.Combine(Local.FullName, "Permissions.yml"));
    }
}