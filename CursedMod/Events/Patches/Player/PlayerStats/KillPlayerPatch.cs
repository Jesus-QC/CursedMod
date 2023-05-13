// -----------------------------------------------------------------------
// <copyright file="KillPlayerPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Player.PlayerStats;

[DynamicEventPatch(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.Dying))]
[HarmonyPatch(typeof(PlayerStatsSystem.PlayerStats), nameof(PlayerStatsSystem.PlayerStats.KillPlayer))]
public class KillPlayerPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<KillPlayerPatch>(37, instructions);

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerDyingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerDying))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerDyingEventArgs), nameof(PlayerDyingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}