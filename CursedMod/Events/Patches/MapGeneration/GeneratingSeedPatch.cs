// -----------------------------------------------------------------------
// <copyright file="GeneratingSeedPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.MapGeneration;
using CursedMod.Events.Handlers.MapGeneration;
using HarmonyLib;
using MapGeneration;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.MapGeneration;

[HarmonyPatch(typeof(SeedSynchronizer), nameof(SeedSynchronizer.Start))]
public class GeneratingSeedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<GeneratingSeedPatch>(37, instructions);

        int offset = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Call);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(GeneratingSeedEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(MapGenerationEventsHandler), nameof(MapGenerationEventsHandler.OnGeneratingSeed))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(GeneratingSeedEventArgs), nameof(GeneratingSeedEventArgs.Seed))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}