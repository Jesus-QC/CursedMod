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
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.LevelingUp))]
[HarmonyPatch(typeof(Scp079TierManager), nameof(Scp079TierManager.AccessTierIndex), MethodType.Setter)]
public class GainLevelPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<GainLevelPatch>(53, instructions);
        
        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ret) + 1;

        Label returnLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(Scp079LevelingUpEventArgs));

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldc_I4_1),
            new (OpCodes.Add),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp079LevelingUpEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnLevelingUp))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079LevelingUpEventArgs), nameof(Scp079LevelingUpEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079LevelingUpEventArgs), nameof(Scp079LevelingUpEventArgs.NewLevel))),
            new (OpCodes.Ldc_I4_1),
            new (OpCodes.Sub),
            new (OpCodes.Starg_S, 1),
        });
        
        newInstructions[newInstructions.Count - 1].WithLabels(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}