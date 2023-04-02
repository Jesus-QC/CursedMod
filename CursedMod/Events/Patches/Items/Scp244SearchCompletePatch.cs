// -----------------------------------------------------------------------
// <copyright file="Scp244SearchCompletePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Items;
using CursedMod.Events.Handlers.Items;
using HarmonyLib;
using InventorySystem.Searching;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.PlayerPickingUpItem))]
[HarmonyPatch(typeof(Scp244SearchCompletor), nameof(Scp244SearchCompletor.Complete))]
public class Scp244SearchCompletePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<Scp244SearchCompletePatch>(28, instructions);

        Label ret = generator.DefineLabel();
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPickingUpItemEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.OnPlayerPickingUpItem))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerPickingUpItemEventArgs), nameof(PlayerPickingUpItemEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}