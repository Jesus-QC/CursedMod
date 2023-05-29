// -----------------------------------------------------------------------
// <copyright file="RemoveTargetPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp096;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Patches.SCPs.Scp096;

[DynamicEventPatch(typeof(CursedScp096EventsHandler), nameof(CursedScp096EventsHandler.RemovingTarget))]
[HarmonyPatch(typeof(Scp096TargetsTracker), nameof(Scp096TargetsTracker.RemoveTarget))]
public class RemoveTargetPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RemoveTargetPatch>(37, instructions);

        Label ret = generator.DefineLabel();
        newInstructions[newInstructions.FindIndex(x => x.opcode == OpCodes.Ldc_I4_1)].labels.Add(ret);
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldarg_0);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp096RemovingTargetEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp096EventsHandler), nameof(CursedScp096EventsHandler.OnRemovingTarget))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp096RemovingTargetEventArgs), nameof(Scp096RemovingTargetEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}