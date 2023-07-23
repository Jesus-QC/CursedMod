// -----------------------------------------------------------------------
// <copyright file="DetonatePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Warhead;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Facility.Warhead;

[DynamicEventPatch(typeof(CursedWarheadEventsHandler), nameof(CursedWarheadEventsHandler.WarheadDetonating))]
[HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.Detonate))]
public class DetonatePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<DetonatePatch>(184, instructions);

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(WarheadDetonatingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedWarheadEventsHandler), nameof(CursedWarheadEventsHandler.OnWarheadDetonating))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(WarheadDetonatingEventArgs), nameof(WarheadDetonatingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}