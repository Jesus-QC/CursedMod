// -----------------------------------------------------------------------
// <copyright file="RespawnManagerPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Respawning;
using CursedMod.Events.Handlers.Respawning;
using HarmonyLib;
using NorthwoodLib.Pools;
using Respawning;

namespace CursedMod.Events.Patches.Respawning;

[HarmonyPatch(typeof(RespawnManager), nameof(RespawnManager.Spawn))]
public class RespawnManagerPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RespawnManagerPatch>(241, instructions);

        Label ret = generator.DefineLabel();
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;

        newInstructions[offset + 11].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(RespawningTeamEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(RespawningEventsHandler), nameof(RespawningEventsHandler.OnRespawningTeam))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(RespawningTeamEventArgs), nameof(RespawningTeamEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}