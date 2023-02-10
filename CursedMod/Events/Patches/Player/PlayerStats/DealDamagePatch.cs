// -----------------------------------------------------------------------
// <copyright file="DealDamagePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Player.PlayerStats;

[HarmonyPatch(typeof(PlayerStatsSystem.PlayerStats), nameof(PlayerStatsSystem.PlayerStats.DealDamage))]
public class DealDamagePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<DealDamagePatch>(214, instructions);

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(offset, new List<CodeInstruction>()
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerReceivingDamageEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerEventHandlers), nameof(PlayerEventHandlers.OnPlayerReceivingDamage))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerReceivingDamageEventArgs), nameof(PlayerReceivingDamageEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}