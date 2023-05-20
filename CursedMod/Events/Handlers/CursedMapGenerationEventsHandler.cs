// -----------------------------------------------------------------------
// <copyright file="CursedMapGenerationEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.MapGeneration;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Facility.Doors;
using CursedMod.Features.Wrappers.Facility.Hazards;
using CursedMod.Features.Wrappers.Facility.Props;
using CursedMod.Features.Wrappers.Facility.Rooms;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Player.Dummies;
using CursedMod.Features.Wrappers.Player.Ragdolls;
using CursedMod.Features.Wrappers.Server;
using UnityEngine.SceneManagement;

namespace CursedMod.Events.Handlers;

public static class CursedMapGenerationEventsHandler
{
    public static event EventManager.CursedEventHandler MapGenerated;
    
    public static event EventManager.CursedEventHandler<GeneratingSeedEventArgs> GeneratingSeed;
    
    internal static void CacheAPI()
    {
        CursedLogger.InternalDebug("Caching api");
        CursedServer.LocalPlayer = new CursedPlayer(ReferenceHub.HostHub);
        CursedWarhead.OutsidePanel = CursedWarhead.Controller.GetComponent<AlphaWarheadOutsitePanel>();
        
        CursedDoor.CacheAllDoors();
        CursedRoom.CacheAllRooms();
        
        MapGenerated.InvokeEvent();
    }

    internal static void OnGeneratingSeed(GeneratingSeedEventArgs args)
    {
        GeneratingSeed.InvokeEvent(args);
    }

    internal static void OnChangingScene(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name != "Facility")
            return;
        
        CursedPlayer.Dictionary.Clear();
        CursedDummy.Dictionary.Clear();
        CursedRagdoll.Dictionary.Clear();
        CursedDoor.Dictionary.Clear();
        CursedLocker.Dictionary.Clear();
        CursedTeslaGate.Dictionary.Clear();
        CursedEnvironmentalHazard.Dictionary.Clear();
        CursedRoom.Dictionary.Clear();
    }
}