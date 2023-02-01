// -----------------------------------------------------------------------
// <copyright file="MapGenerationEventHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Player.Dummies;
using CursedMod.Features.Wrappers.Server;
using PluginAPI.Core;
using UnityEngine.SceneManagement;

namespace CursedMod.Events.Handlers.MapGeneration;

public static class MapGenerationEventHandler
{
    public static void CacheAPI()
    {
        CursedLogger.InternalDebug("Caching api");
        CursedWarhead.OutsidePanel = CursedWarhead.Controller.GetComponent<AlphaWarheadOutsitePanel>();
        CursedServer.LocalPlayer = new CursedPlayer(ReferenceHub.HostHub);
    }

    public static void OnChangingScene(Scene scene, LoadSceneMode loadMode)
    {
        CursedLogger.InternalDebug("Loading scene " + scene.name + " with mode " + loadMode);
        if (scene.name != "Facility")
            return;
        
        CursedPlayer.Dictionary.Clear();
        CursedDummy.Dictionary.Clear();
    }
}