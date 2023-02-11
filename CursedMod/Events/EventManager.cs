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
using HarmonyLib;
using MapGeneration;
using NorthwoodLib.Pools;
using PluginAPI.Core;
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
            Log.Warning("Patching events.");
            
            Stopwatch watch = Stopwatch.StartNew();
            
            Harmony.PatchAll();
            
            Log.Warning("Events patched in " + watch.Elapsed.ToString("c"));

            foreach (MethodBase patch in Harmony.GetPatchedMethods())
            {
                Log.Debug(patch.DeclaringType + "::" + patch.Name);
            }

            RegisterHookedEvents();
        }
        catch (Exception e)
        {
            Log.Error("An exception occurred when patching the events.");
            Log.Error(e.ToString());
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
                Log.Error("An error occurred while handling the event " + eventHandler.GetType().Name);
                Log.Error(e.ToString());
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
                Log.Error("An error occurred while handling the event " + eventHandler.GetType().Name);
                Log.Error(e.ToString());
                throw;
            }
        }
    }

    public static List<CodeInstruction> CheckEvent<T>(int originalCodes, IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
        
        if (originalCodes == newInstructions.Count)
            return newInstructions;
        
        Log.Error(typeof(T).FullDescription() + $" has an incorrect number of OpCodes ({originalCodes} != {newInstructions.Count}). The patch may be broken or bugged.");
        return newInstructions;
    }

    private static void RegisterHookedEvents()
    {
        SceneManager.sceneLoaded += MapGenerationEventsHandler.OnChangingScene;
        SeedSynchronizer.OnMapGenerated += MapGenerationEventsHandler.CacheAPI;
    }
}