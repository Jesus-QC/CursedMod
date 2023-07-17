// -----------------------------------------------------------------------
// <copyright file="PlaceBloodDecalPatch.cs" company="CursedMod">
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
using InventorySystem.Items.Firearms.Modules;
using NorthwoodLib.Pools;
using UnityEngine;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.PlacingBloodDecal))]
[HarmonyPatch(typeof(StandardHitregBase), nameof(StandardHitregBase.PlaceBloodDecal))]
public class PlaceBloodDecalPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<PlaceBloodDecalPatch>(22, instructions);
        
        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Newobj) - 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPlacingBloodDecalEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerPlacingBloodDecal))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerPlacingBloodDecalEventArgs), nameof(PlayerPlacingBloodDecalEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}