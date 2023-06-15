// -----------------------------------------------------------------------
// <copyright file="CursedEventManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using CursedMod.Events.Handlers;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Loader.Configurations;
using HarmonyLib;
using MapGeneration;
using NorthwoodLib.Pools;
using PlayerRoles.Ragdolls;
using PluginAPI.Core;
using UnityEngine.SceneManagement;

namespace CursedMod.Events;

public static class CursedEventManager
{
    private static readonly Harmony Harmony = new ("com.jesusqc.cursedmod");
    
    public delegate void CursedEventHandler<in T>(T ev)
        where T : EventArgs;
    
    public delegate void CursedEventHandler();
    
    public static void PatchEvents()
    {
        Stopwatch watch = Stopwatch.StartNew();

        if (CursedModConfigurationManager.LoadedConfiguration.UseDynamicPatching)
        {
            CursedLogger.InternalPrint("CursedMod is using dynamic patching.");
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!type.IsClass)
                    continue;

                try
                {
                    if (TryDynamicPatching(type))
                        continue;

                    Harmony.CreateClassProcessor(type).Patch();
                }
                catch (HarmonyException e)
                {
                    CursedLogger.LogError("There was an error while patching the class " + type.FullName);
                    CursedLogger.LogError(e.ToString());
                }
            }
        }
        else
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!type.IsClass)
                    continue;

                try
                {
                    Harmony.CreateClassProcessor(type).Patch();
                }
                catch (HarmonyException e)
                {
                    CursedLogger.LogError("There was an error while patching the class " + type.FullName);
                    CursedLogger.LogError(e.ToString());
                }
            }
        }
#if DEBUG
            foreach (MethodBase patch in Harmony.GetPatchedMethods())
            {
                CursedLogger.InternalDebug(patch.DeclaringType + "::" + patch.Name);
            }
#endif
        watch.Stop();
        CursedLogger.InternalPrint("Events patched in " + watch.Elapsed.ToString("c"));

        RegisterHookedEvents();
    }

    public static void InvokeEvent<T>(this CursedEventHandler<T> eventHandler, T args)
        where T : EventArgs
    {
        if (eventHandler is null)
            return;
        
        foreach (Delegate sub in eventHandler.GetInvocationList())
        {
            try
            {
                sub.DynamicInvoke(args);
            }
            catch (Exception e)
            {
                CursedLogger.LogError("An error occurred while handling the event " + eventHandler.GetType().Name);
                CursedLogger.LogError(e.ToString());
                throw;
            }
        }
    }
    
    public static void InvokeEvent(this CursedEventHandler eventHandler)
    {
        if (eventHandler is null)
            return;
        
        foreach (Delegate sub in eventHandler.GetInvocationList())
        {
            try
            {
                sub.DynamicInvoke();
            }
            catch (Exception e)
            {
                CursedLogger.LogError("An error occurred while handling the event " + eventHandler.GetType().Name);
                CursedLogger.LogError(e.ToString());
                throw;
            }
        }
    }

    public static List<CodeInstruction> CheckEvent<T>(int originalCodes, IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
        
        if (originalCodes == newInstructions.Count)
            return newInstructions;
        
        CursedLogger.LogError(typeof(T).FullDescription() + $" has an incorrect number of OpCodes ({originalCodes} != {newInstructions.Count}). The patch may be broken or bugged.");
        return newInstructions;
    }

    internal static bool CheckPlayer(this CursedPlayer player) => player is not null && !player.IsDummy;
    
    private static void RegisterHookedEvents()
    {
        SceneManager.sceneLoaded += CursedMapGenerationEventsHandler.OnChangingScene;
        SeedSynchronizer.OnMapGenerated += CursedMapGenerationEventsHandler.CacheAPI;
        RagdollManager.OnRagdollSpawned += CursedFacilityEventsHandler.OnRagdollSpawned;
    }
    
    private static bool TryDynamicPatching(Type type)
    {
        bool isDynamicEvent = false;
        foreach (Attribute attribute in type.GetCustomAttributes())
        {
            if (attribute is not DynamicEventPatchAttribute dynamicEventAttribute)
                continue;

            isDynamicEvent = true;
            
            object value = dynamicEventAttribute.EventInfo.GetValue(null);

            if (value is null)
                continue;

            Harmony.CreateClassProcessor(type).Patch();
            return true;
        }

        return isDynamicEvent;
    }
}