// -----------------------------------------------------------------------
// <copyright file="CancelCloudPlacementPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.CancellingCloudPlacement))]
[HarmonyPatch(typeof(Scp939AmnesticCloudAbility), nameof(Scp939AmnesticCloudAbility.OnStateDisabled))]
public class CancelCloudPlacementPatch
{
    // TODO: REVIEW
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<CancelCloudPlacementPatch>(4, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp939CancellingCloudPlacementEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnCancellingCloudPlacement))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp939CancellingCloudPlacementEventArgs), nameof(Scp939CancellingCloudPlacementEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}