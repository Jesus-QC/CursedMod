// -----------------------------------------------------------------------
// <copyright file="UsableItemUpdatePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers;
using HarmonyLib;
using InventorySystem.Items.Usables;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items.Usables;

// TODO
// [DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerCancelledUsable))]
// [DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerUsedItem))]
// [HarmonyPatch(typeof(UsableItemsController), nameof(UsableItemsController.ServerReceivedStatus))]
public class UsableItemUpdatePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<UsableItemReceivedStatusPatch>(145, instructions);

        Label ret = generator.DefineLabel();

        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int offset = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Brtrue_S) - 6;
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}