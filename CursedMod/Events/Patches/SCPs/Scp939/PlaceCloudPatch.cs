// -----------------------------------------------------------------------
// <copyright file="PlaceCloudPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp939;
using CursedMod.Events.Handlers.SCPs.Scp939;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(Scp939EventsHandler), nameof(Scp939EventsHandler.PlayerPlaceAmnesticCloud))]
[HarmonyPatch(typeof(Scp939AmnesticCloudAbility), nameof(Scp939AmnesticCloudAbility.OnStateEnabled))]
public class PlaceCloudPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<PlaceCloudPatch>(34, instructions);
        
        Label returnLabel = generator.DefineLabel();

        const int offset = -2;
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Callvirt) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPlaceAmnesticCloudEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp939EventsHandler), nameof(Scp939EventsHandler.OnPlayerPlaceAmnesticCloud))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerPlaceAmnesticCloudEventArgs), nameof(PlayerPlaceAmnesticCloudEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}