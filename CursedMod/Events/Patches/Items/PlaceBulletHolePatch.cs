// -----------------------------------------------------------------------
// <copyright file="PlaceBulletHolePatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.PlacingBulletHole))]
[HarmonyPatch(typeof(StandardHitregBase), nameof(StandardHitregBase.PlaceBulletholeDecal))]
public class PlaceBulletHolePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<PlaceBulletHolePatch>(13, instructions);
        
        Label retLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPlacingBulletHoleEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerPlacingBulletHole))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerPlacingBulletHoleEventArgs), nameof(PlayerPlacingBulletHoleEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}