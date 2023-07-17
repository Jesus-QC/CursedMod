// -----------------------------------------------------------------------
// <copyright file="ElevatorChamberUpdatePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Elevators;
using CursedMod.Events.Handlers;
using HarmonyLib;
using Interactables.Interobjects;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Facility.Elevators;

[DynamicEventPatch(typeof(CursedElevatorsEventHandler), nameof(CursedElevatorsEventHandler.ElevatorMoving))]
[HarmonyPatch(typeof(ElevatorChamber), nameof(ElevatorChamber.Update))]
public class ElevatorChamberUpdatePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<ElevatorChamberUpdatePatch>(83, instructions);
        
        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Ldsfld &&
            i.OperandIs(AccessTools.Field(typeof(ElevatorChamber), nameof(ElevatorChamber.OnElevatorMoved)))) + 9;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloc_2),
            new (OpCodes.Ldloc_S, 5),
            new (OpCodes.Ldloc_S, 6),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(ElevatorMovingEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedElevatorsEventHandler), nameof(CursedElevatorsEventHandler.OnElevatorMoving))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}