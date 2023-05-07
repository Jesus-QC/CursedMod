// -----------------------------------------------------------------------
// <copyright file="CorpseConsumedPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp0492;
using CursedMod.Events.Handlers.SCPs.Scp0492;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049.Zombies;

namespace CursedMod.Events.Patches.SCPs.Scp0492;

[DynamicEventPatch(typeof(ZombieEventsHandler), nameof(ZombieEventsHandler.PlayerCorpseConsumed))]
[HarmonyPatch(typeof(ZombieConsumeAbility), nameof(ZombieConsumeAbility.ServerComplete))]
public class CorpseConsumedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<CorpseConsumedPatch>(18, instructions);

        const int offset = 1;
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerCorpseConsumedEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(ZombieEventsHandler), nameof(ZombieEventsHandler.OnPlayerCorpseConsumed))),
        });
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}