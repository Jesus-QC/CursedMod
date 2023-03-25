// -----------------------------------------------------------------------
// <copyright file="CursedPaths.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace CursedMod.Loader;

public static class CursedPaths
{
    public static DirectoryInfo MainPath { get; private set; }
    
    public static DirectoryInfo PluginsPath { get; private set; }

    public static DirectoryInfo DependenciesPath { get; private set; }

    public static void LoadPaths(string mainPath)
    {
        MainPath = Directory.CreateDirectory(mainPath);
        PluginsPath = Directory.CreateDirectory(Path.Combine(mainPath, "Plugins"));
        DependenciesPath = Directory.CreateDirectory(Path.Combine(mainPath, "Dependencies"));
    }
}