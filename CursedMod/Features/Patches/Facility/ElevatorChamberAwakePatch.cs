// -----------------------------------------------------------------------
// <copyright file="ElevatorChamberAwakePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events;
using CursedMod.Features.Wrappers.Facility.Elevators;
using HarmonyLib;
using Interactables.Interobjects;
using NorthwoodLib.Pools;

namespace CursedMod.Features.Patches.Facility;

[HarmonyPatch(typeof(ElevatorChamber), nameof(ElevatorChamber.Awake))]
public class ElevatorChamberAwakePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ElevatorChamberAwakePatch>(8, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedElevatorChamber), nameof(CursedElevatorChamber.Get))),
            new (OpCodes.Pop),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}