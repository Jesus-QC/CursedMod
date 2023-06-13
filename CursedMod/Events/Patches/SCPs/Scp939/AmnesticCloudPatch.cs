// -----------------------------------------------------------------------
// <copyright file="AmnesticCloudPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp939;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlacingAmnesticCloud))]
[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.CancellingCloudPlacement))]
[HarmonyPatch(typeof(Scp939AmnesticCloudAbility), nameof(Scp939AmnesticCloudAbility.TargetState), MethodType.Setter)]
public class AmnesticCloudPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<AmnesticCloudPatch>(4, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(AmnesticCloudPatch), nameof(CallEvent))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool CallEvent(Scp939AmnesticCloudAbility ability, bool value)
    {
        if (value)
        {
            Scp939PlacingAmnesticCloudEventArgs args = new (ability);
            CursedScp939EventsHandler.OnPlacingAmnesticCloud(args);
            return args.IsAllowed;
        }
        else
        {
            Scp939CancellingCloudPlacementEventArgs args = new (ability);
            CursedScp939EventsHandler.OnCancellingCloudPlacement(args);
            return args.IsAllowed;
        }
    }
}