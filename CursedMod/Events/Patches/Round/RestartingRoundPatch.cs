// -----------------------------------------------------------------------
// <copyright file="RestartingRoundPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using RoundRestarting;

namespace CursedMod.Events.Patches.Round;

[DynamicEventPatch(typeof(CursedRoundEventsHandler), nameof(CursedRoundEventsHandler.RestartingRound))]
[HarmonyPatch(typeof(RoundRestart), nameof(RoundRestart.InitiateRoundRestart))]
public class RestartingRoundPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<RestartingRoundPatch>(91, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Call, AccessTools.Method(typeof(CursedRoundEventsHandler), nameof(CursedRoundEventsHandler.OnRestartingRound))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}