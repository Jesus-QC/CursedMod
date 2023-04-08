// -----------------------------------------------------------------------
// <copyright file="CreateMatchPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers.Server;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Server;

[HarmonyPatch(typeof(CustomNetworkManager), nameof(CustomNetworkManager.CreateMatch))]
public class CreateMatchPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<CreateMatchPatch>(175, instructions);

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new (OpCodes.Call, AccessTools.Method(typeof(ServerEventsHandler), nameof(ServerEventsHandler.OnLoadedConfigs))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}