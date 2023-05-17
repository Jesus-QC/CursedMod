// -----------------------------------------------------------------------
// <copyright file="BloodlustingPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp0492;
using CursedMod.Events.Handlers;
using CursedMod.Features.Wrappers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049.Zombies;

namespace CursedMod.Events.Patches.SCPs.Scp0492;

[DynamicEventPatch(typeof(CursedZombieEventsHandler), nameof(CursedZombieEventsHandler.PlayerBloodlusting))]
[HarmonyPatch(typeof(ZombieBloodlustAbility), nameof(ZombieBloodlustAbility.AnyTargets))]
public class BloodlustingPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<BloodlustingPatch>(56, instructions);
        
        Label retLabel = generator.DefineLabel();

        const int offset = -1;
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_S) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerBloodlustingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedZombieEventsHandler), nameof(CursedZombieEventsHandler.OnPlayerBloodlusting))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerBloodlustingEventArgs), nameof(PlayerBloodlustingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}