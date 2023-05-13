// -----------------------------------------------------------------------
// <copyright file="UsingCompletedPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerUsedItem))]
[HarmonyPatch(typeof(Consumable), nameof(Consumable.ServerOnUsingCompleted))] // Todo: add scp268 scp244 scp1576 and scp330 because they use a different method
public class UsingCompletedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<UsingCompletedPatch>(16, instructions);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUsedItemEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerUsedItem))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}