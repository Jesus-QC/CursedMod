// -----------------------------------------------------------------------
// <copyright file="ReloadServerNamePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events;
using CursedMod.Loader;
using CursedMod.Loader.Configurations;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Features.Patches;

// todo: disabled until unity 2019
// [HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
public class ReloadServerNamePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ReloadServerNamePatch>(6, instructions);
        Label ret = generator.DefineLabel();

        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new (OpCodes.Ldsfld, AccessTools.Field(typeof(EntryPoint), nameof(EntryPoint.ModConfiguration))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(CursedModConfiguration), nameof(CursedModConfiguration.ShowCursedModVersion))),
            new (OpCodes.Brfalse_S, ret),
            
            new (OpCodes.Ldsfld, AccessTools.Field(typeof(ServerConsole), nameof(ServerConsole._serverName))),
            new (OpCodes.Ldstr, $"<size=-1>CursedMod {CursedModInformation.Version}</size>"),
            new (OpCodes.Call, AccessTools.Method(typeof(string), nameof(string.Concat), new[] { typeof(string), typeof(string) })),
            new (OpCodes.Stsfld, AccessTools.Field(typeof(ServerConsole), nameof(ServerConsole._serverName))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
