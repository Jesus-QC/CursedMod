// -----------------------------------------------------------------------
// <copyright file="DropItemPatch.cs" company="CursedMod">
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
using InventorySystem;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.PlayerDroppingItem))]
[HarmonyPatch(typeof(Inventory), nameof(Inventory.UserCode_CmdDropItem))]
public class DropItemPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<DropItemPatch>(174, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(PlayerDroppingItemEventArgs));
        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        CodeInstruction offsetInstruction = newInstructions[offset];
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(offsetInstruction),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerDroppingItemEventArgs))[0]),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.OnPlayerDroppingItem))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerDroppingItemEventArgs), nameof(PlayerDroppingItemEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerDroppingItemEventArgs), nameof(PlayerDroppingItemEventArgs.IsThrow))),
            new (OpCodes.Starg_S, 2),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}