// -----------------------------------------------------------------------
// <copyright file="Scp330SearchCompletePatch.cs" company="CursedMod">
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
using InventorySystem.Searching;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerPickingUpItem))]
[HarmonyPatch(typeof(Scp330SearchCompletor), nameof(Scp330SearchCompletor.Complete))]
public class Scp330SearchCompletePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<Scp244SearchCompletePatch>(47, instructions);

        Label ret = generator.DefineLabel();
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPickingUpItemEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerPickingUpItem))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerPickingUpItemEventArgs), nameof(PlayerPickingUpItemEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}