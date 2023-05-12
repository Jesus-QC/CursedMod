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
using CursedMod.Events.Handlers.Facility.Warhead;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Facility.Warhead;

[DynamicEventPatch(typeof(WarheadEventsHandler), nameof(WarheadEventsHandler.PlayerStartingDetonation))]
[HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.StartDetonation))]
public class StartDetonationPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<StartDetonationPatch>(119, instructions);

        Label ret = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(PlayerStartingDetonationEventArgs));
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(newInstructions.FindIndex(x => x.opcode == OpCodes.Ret), new CodeInstruction[] 
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerStartingDetonationEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(WarheadEventsHandler), nameof(WarheadEventsHandler.OnPlayerStartingDetonation))),
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