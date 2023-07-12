// -----------------------------------------------------------------------
// <copyright file="EnteringTantrumPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Hazards;
using CursedMod.Events.Handlers;
using HarmonyLib;
using Hazards;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Facility.Hazards;

[DynamicEventPatch(typeof(CursedHazardsEventHandler), nameof(CursedHazardsEventHandler.EnteringHazard))]
[HarmonyPatch(typeof(SinkholeEnvironmentalHazard), nameof(TantrumEnvironmentalHazard.OnEnter))]
public class EnteringTantrumPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<EnteringTantrumPatch>(18, instructions);
        
        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.Calls(AccessTools.Method(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.OnEnter)))) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerEnteringHazardEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedHazardsEventHandler), nameof(CursedHazardsEventHandler.OnEnteringHazard))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEnteringHazardEventArgs), nameof(PlayerEnteringHazardEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}