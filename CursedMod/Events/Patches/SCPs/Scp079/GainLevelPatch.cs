// -----------------------------------------------------------------------
// <copyright file="GainLevelPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp079;
using CursedMod.Events.Handlers.SCPs.Scp079;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.PlayerLevelUp))]
[HarmonyPatch(typeof(Scp079TierManager), nameof(Scp079TierManager.AccessTierIndex), MethodType.Setter)]
public class GainLevelPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<GainLevelPatch>(62, instructions);

        const int offset = 1;
        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ret) + offset;

        Label returnLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(PlayerLevelUpEventArgs));

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldc_I4_1),
            new (OpCodes.Add),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerLevelUpEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.OnPlayerLevelUp))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerLevelUpEventArgs), nameof(PlayerLevelUpEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerLevelUpEventArgs), nameof(PlayerLevelUpEventArgs.Level))),
            new (OpCodes.Ldc_I4_1),
            new (OpCodes.Sub),
            new (OpCodes.Starg_S, 1),
        });
        
        newInstructions[newInstructions.Count - 1].WithLabels(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}