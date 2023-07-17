// -----------------------------------------------------------------------
// <copyright file="StartDetonationPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Warhead;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Facility.Warhead;

[DynamicEventPatch(typeof(CursedWarheadEventsHandler), nameof(CursedWarheadEventsHandler.PlayerStartingDetonation))]
[HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.StartDetonation))]
public class StartDetonationPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<StartDetonationPatch>(111, instructions);

        Label ret = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(PlayerStartingDetonationEventArgs));

        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(index, new CodeInstruction[] 
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerStartingDetonationEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedWarheadEventsHandler), nameof(CursedWarheadEventsHandler.OnPlayerStartingDetonation))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerStartingDetonationEventArgs), nameof(PlayerStartingDetonationEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerStartingDetonationEventArgs), nameof(PlayerStartingDetonationEventArgs.IsAutomatic))),
            new (OpCodes.Starg_S, 0),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerStartingDetonationEventArgs), nameof(PlayerStartingDetonationEventArgs.SuppressSubtitles))),
            new (OpCodes.Starg_S, 1),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}