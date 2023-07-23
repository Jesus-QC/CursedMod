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
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049.Zombies;

namespace CursedMod.Events.Patches.SCPs.Scp0492;

[DynamicEventPatch(typeof(CursedScp0492EventsHandler), nameof(CursedScp0492EventsHandler.ConsumedCorpse))]
[HarmonyPatch(typeof(ZombieConsumeAbility), nameof(ZombieConsumeAbility.ServerComplete))]
public class CorpseConsumedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<CorpseConsumedPatch>(18, instructions);

        LocalBuilder health = generator.DeclareLocal(typeof(float));
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        CodeInstruction i = newInstructions.Find(x => x.opcode == OpCodes.Ldc_R4);
        i.opcode = OpCodes.Ldloc_S;
        i.operand = health.LocalIndex;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp0492ConsumedCorpseEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp0492EventsHandler), nameof(CursedScp0492EventsHandler.OnConsumedCorpse))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp0492ConsumedCorpseEventArgs), nameof(Scp0492ConsumedCorpseEventArgs.Health))),
            new (OpCodes.Stloc_S, health.LocalIndex),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}