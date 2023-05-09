// -----------------------------------------------------------------------
// <copyright file="RoundStartPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers.Round;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Round;

[DynamicEventPatch(typeof(RoundEventsHandler), nameof(RoundEventsHandler.RoundStarted))]
[HarmonyPatch(typeof(CharacterClassManager), nameof(CharacterClassManager.RpcRoundStarted))]
public class RoundStartPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RoundStartPatch>(12, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Call, AccessTools.Method(typeof(RoundEventsHandler), nameof(RoundEventsHandler.OnRoundStarted))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}