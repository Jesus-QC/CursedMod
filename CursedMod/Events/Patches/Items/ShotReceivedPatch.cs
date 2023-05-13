// -----------------------------------------------------------------------
// <copyright file="ShotReceivedPatch.cs" company="CursedMod">
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
using InventorySystem.Items.Firearms.BasicMessages;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerShooting))]
[HarmonyPatch(typeof(FirearmBasicMessagesHandler), nameof(FirearmBasicMessagesHandler.ServerShotReceived))]
public class ShotReceivedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ShotReceivedPatch>(44, instructions);

        Label ret = generator.DefineLabel();

        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldloc_1) + 8;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerShootingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerShooting))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerShootingEventArgs), nameof(PlayerShootingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}