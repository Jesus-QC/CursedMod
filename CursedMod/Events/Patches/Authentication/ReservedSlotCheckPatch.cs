// -----------------------------------------------------------------------
// <copyright file="ReservedSlotCheckPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Authentication;
using CursedMod.Events.Handlers.Authentication;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Authentication;

[HarmonyPatch(typeof(ReservedSlot), nameof(ReservedSlot.HasReservedSlot))]
public class ReservedSlotCheckPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ReservedSlotCheckPatch>(37, instructions);

        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Stloc_1);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(CheckingReservedSlotEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(AuthenticationEventsHandler), nameof(AuthenticationEventsHandler.OnCheckingReservedSlot))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(CheckingReservedSlotEventArgs), nameof(CheckingReservedSlotEventArgs.HasReservedSlot))),
            new (OpCodes.Stloc_0),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(CheckingReservedSlotEventArgs), nameof(CheckingReservedSlotEventArgs.CheckReservedSlotCancellationData))),
            new (OpCodes.Stloc_1),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}