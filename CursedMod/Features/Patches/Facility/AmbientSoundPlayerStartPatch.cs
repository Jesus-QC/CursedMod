// -----------------------------------------------------------------------
// <copyright file="AmbientSoundPlayerStartPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events;
using CursedMod.Features.Wrappers.Facility;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Features.Patches.Facility;

[HarmonyPatch(typeof(AmbientSoundPlayer), nameof(AmbientSoundPlayer.Start))]
public class AmbientSoundPlayerStartPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<AmbientSoundPlayerStartPatch>(31, instructions);

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0), 
            new (OpCodes.Call, AccessTools.PropertySetter(typeof(CursedFacility), nameof(CursedFacility.AmbientSoundPlayer))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}