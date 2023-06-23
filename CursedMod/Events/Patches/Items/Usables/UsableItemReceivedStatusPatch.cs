// -----------------------------------------------------------------------
// <copyright file="UsableItemReceivedStatusPatch.cs" company="CursedMod">
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
using InventorySystem.Items.Usables;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items.Usables;

// TODO: REWRITE
// [DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerCancellingUsable))]
// [DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerUsingItem))]
// [HarmonyPatch(typeof(UsableItemsController), nameof(UsableItemsController.ServerReceivedStatus))]
public class UsableItemReceivedStatusPatch
{
    /*private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<UsableItemReceivedStatusPatch>(145, instructions);

        Label ret = generator.DefineLabel();
        int offset = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Newarr) - 2;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerCancellingUsableEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerCancellingUsable))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerCancellingUsableEventArgs), nameof(PlayerCancellingUsableEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Newarr) - 2;
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUsingItemEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerUsingItem))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUsingItemEventArgs), nameof(PlayerUsingItemEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }*/
}