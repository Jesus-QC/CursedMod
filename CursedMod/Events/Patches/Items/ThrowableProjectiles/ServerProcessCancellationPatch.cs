// -----------------------------------------------------------------------
// <copyright file="ServerProcessCancellationPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Items;
using CursedMod.Events.Handlers;
using HarmonyLib;
using InventorySystem.Items.ThrowableProjectiles;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items.ThrowableProjectiles;

[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerCancellingThrow))]
[HarmonyPatch(typeof(ThrowableItem), nameof(ThrowableItem.ServerProcessCancellation))]
public class ServerProcessCancellationPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerProcessCancellationPatch>(30, instructions);

        Label ret = generator.DefineLabel();
        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Bgt_Un_S) + 2;

        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(index, new List<CodeInstruction>
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerCancellingThrowEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerCancellingThrow))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerCancellingThrowEventArgs), nameof(PlayerCancellingThrowEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}