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
    public static DirectoryInfo MainPath { get; private set; }
    
    public static DirectoryInfo GlobalPath { get; private set; }
    
    public static DirectoryInfo LocalPath { get; private set; }
    
    public static DirectoryInfo GlobalPluginsPath { get; private set; }
    
    public static DirectoryInfo GlobalDependenciesPath { get; private set; }
    
    public static DirectoryInfo LocalPluginsPath { get; private set; }

    public static DirectoryInfo LocalDependenciesPath { get; private set; }

    public static void LoadPaths()
    {
        MainPath = Directory.CreateDirectory(Path.Combine(Paths.PluginAPI, "CursedMod"));
        GlobalPath = MainPath.CreateSubdirectory("Global");
        LocalPath = MainPath.CreateSubdirectory(CursedServer.Port.ToString());

        GlobalPluginsPath = GlobalPath.CreateSubdirectory("Plugins");
        GlobalDependenciesPath = GlobalPath.CreateSubdirectory("Dependencies");
        
        LocalPluginsPath = LocalPath.CreateSubdirectory("Plugins");
        LocalDependenciesPath = LocalPath.CreateSubdirectory("Dependencies");
    }
}