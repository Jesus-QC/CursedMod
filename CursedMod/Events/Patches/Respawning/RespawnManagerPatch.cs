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
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using Respawning;

namespace CursedMod.Events.Patches.Respawning;

[DynamicEventPatch(typeof(CursedRespawningEventsHandler), nameof(CursedRespawningEventsHandler.RespawningTeam))]
[HarmonyPatch(typeof(RespawnManager), nameof(RespawnManager.Spawn))]
public class RespawnManagerPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<RespawnManagerPatch>(299, instructions);

        Label ret = generator.DefineLabel();
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;

        newInstructions[offset + 11].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new[]
        {
            // TODO: Add selected players in another event
            new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(RespawningTeamEventArgs))[0]).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedRespawningEventsHandler), nameof(CursedRespawningEventsHandler.OnRespawningTeam))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(RespawningTeamEventArgs), nameof(RespawningTeamEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}