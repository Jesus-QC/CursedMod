// -----------------------------------------------------------------------
// <copyright file="StayingOnHazardPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedHazardsEventHandler), nameof(CursedHazardsEventHandler.StayingOnHazard))]
[HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.OnStay))]
public class StayingOnHazardPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<StayingOnHazardPatch>(1, instructions);
        
        Label retLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).WithLabels(newInstructions[0].labels),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerStayingOnHazardEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedHazardsEventHandler), nameof(CursedHazardsEventHandler.OnStayingOnHazard))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}