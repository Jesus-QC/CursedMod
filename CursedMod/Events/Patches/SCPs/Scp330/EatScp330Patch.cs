﻿// -----------------------------------------------------------------------
// <copyright file="EatScp330Patch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp330;
using CursedMod.Events.Handlers;
using HarmonyLib;
using InventorySystem.Items.Usables.Scp330;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.SCPs.Scp330;

[DynamicEventPatch(typeof(CursedScp330EventsHandler), nameof(CursedScp330EventsHandler.PlayerEatingScp330))]
[HarmonyPatch(typeof(Scp330Bag), nameof(Scp330Bag.ServerOnUsingCompleted))]
public class EatScp330Patch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<EatScp330Patch>(33, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(i => i.Calls(AccessTools.Method(typeof(ICandy), nameof(ICandy.ServerApplyEffects)))) - 3;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerEatingScp330EventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp330EventsHandler), nameof(CursedScp330EventsHandler.OnPlayerEatingScp330))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEatingScp330EventArgs), nameof(PlayerEatingScp330EventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}