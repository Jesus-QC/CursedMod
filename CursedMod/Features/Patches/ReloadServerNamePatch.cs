// -----------------------------------------------------------------------
// <copyright file="ReloadServerNamePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Loader;
using CursedMod.Loader.Configurations;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Server;

[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
public class ReloadServerNamePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ReloadServerNamePatch>(6, instructions);
        Label ret = generator.DefineLabel();

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new (OpCodes.Call, AccessTools.Field(typeof(EntryPoint), nameof(EntryPoint.ModConfiguration))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(CursedModConfiguration), nameof(CursedModConfiguration.ShowCursedModVersion))),
            new (OpCodes.Brfalse_S, ret),

            new (OpCodes.Ldstr, $"{ServerConsole._serverName}<color=#00000000><size=1>CursedMod {CursedModInformation.Version}</size></color>"),
            new (OpCodes.Stsfld, AccessTools.Field(typeof(ServerConsole), nameof(ServerConsole._serverName))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
