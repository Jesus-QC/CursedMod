// -----------------------------------------------------------------------
// <copyright file="ServerEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Loader.Permissions;

namespace CursedMod.Events.Handlers.Server;

public static class ServerEventsHandler
{
    public static event EventManager.CursedEventHandler LoadedConfigs;

    internal static void OnLoadedConfigs()
    {
        PermissionsManager.Load();
        LoadedConfigs.InvokeEvent();
    }
}