// -----------------------------------------------------------------------
// <copyright file="EventManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using CursedMod.Events.Handlers.MapGeneration;
using CursedMod.Events.Handlers.Player;
using CursedMod.Features.Logger;
using HarmonyLib;
using MapGeneration;
using NorthwoodLib.Pools;
using PlayerRoles.Ragdolls;
using UnityEngine.SceneManagement;

namespace CursedMod.Events;

public static class EventManager
{
    private static readonly Harmony Harmony = new ("com.jesusqc.cursedmod");
    
    public delegate void CursedEventHandler<in T>(T ev)
        where T : EventArgs;
    
    public delegate void CursedEventHandler();
    
    public static void PatchEvents()
    {
        try
        {
            Stopwatch watch = Stopwatch.StartNew();
#if !DEBUG
            if (EntryPoint.ModConfiguration.UseDynamicPatching)
            {
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (!type.IsClass)
                        continue;
                    
                    if (TryDynamicPatching(type))
                        continue;
                
                    Harmony.CreateClassProcessor(type).Patch();
                }
            }
            else
            {
                Harmony.PatchAll();
            }
#else
            
            foreach (MethodBase patch in Harmony.GetPatchedMethods())
            {
                CursedLogger.InternalDebug(patch.DeclaringType + "::" + patch.Name);
            }
#endif
            watch.Stop();
            CursedLogger.InternalPrint("Events patched in " + watch.Elapsed.ToString("c"));

            RegisterHookedEvents();
        }
        catch (Exception e)
        {
            CursedLogger.LogError("An exception occurred when patching the events.");
            CursedLogger.LogError(e.ToString());
        }
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

    private static void RegisterHookedEvents()
    {
        SceneManager.sceneLoaded += MapGenerationEventsHandler.OnChangingScene;
        SeedSynchronizer.OnMapGenerated += MapGenerationEventsHandler.CacheAPI;
        RagdollManager.OnRagdollSpawned += PlayerEventsHandler.OnRagdollSpawned;
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