// -----------------------------------------------------------------------
// <copyright file="HuntersAtlasPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp106;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Events.Patches.SCPs.Scp106;

[DynamicEventPatch(typeof(CursedScp106EventsHandler), nameof(CursedScp106EventsHandler.Submerging))]
[DynamicEventPatch(typeof(CursedScp106EventsHandler), nameof(CursedScp106EventsHandler.ExitingSubmergence))]
[HarmonyPatch(typeof(Scp106HuntersAtlasAbility), nameof(Scp106HuntersAtlasAbility.SetSubmerged), typeof(bool))]
public class HuntersAtlasPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<HuntersAtlasPatch>(25, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(HuntersAtlasPatch), nameof(ProcessSubmergingEvents))),
            new (OpCodes.Brfalse_S, returnLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool ProcessSubmergingEvents(Scp106HuntersAtlasAbility atlasAbility, bool submerging)
    {
        if (submerging)
        {
            Scp106SubmergingEventArgs submergingArgs = new (atlasAbility);
            CursedScp106EventsHandler.OnSubmerging(submergingArgs);
            return submergingArgs.IsAllowed;
        }
        
        Scp106ExitingSubmergenceEventArgs exitingArgs = new (atlasAbility);
        CursedScp106EventsHandler.OnExitingSubmergence(exitingArgs);
        return exitingArgs.IsAllowed;
    }
}