// -----------------------------------------------------------------------
// <copyright file="ProcessSenseAbilityPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp049;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Patches.SCPs.Scp049;

[DynamicEventPatch(typeof(CursedScp049EventsHandler), nameof(CursedScp049EventsHandler.UsingSenseAbility))]
[HarmonyPatch(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility.ServerProcessCmd))]
public class ProcessSenseAbilityPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ProcessSenseAbilityPatch>(75, instructions);

        Label returnLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(Scp049UsingSenseAbilityEventArgs));
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_0) - 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp049UsingSenseAbilityEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp049EventsHandler), nameof(CursedScp049EventsHandler.OnUsingSenseAbility))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp049UsingSenseAbilityEventArgs), nameof(Scp049UsingSenseAbilityEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp049UsingSenseAbilityEventArgs), nameof(Scp049UsingSenseAbilityEventArgs.Distance))),
            new (OpCodes.Stfld, AccessTools.Field(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility._distanceThreshold))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}