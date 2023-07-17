// -----------------------------------------------------------------------
// <copyright file="ReportingPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.BanSystem;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.BanSystem;

[DynamicEventPatch(typeof(CursedBanSystemEventsHandler), nameof(CursedBanSystemEventsHandler.LocalReporting))]
[DynamicEventPatch(typeof(CursedBanSystemEventsHandler), nameof(CursedBanSystemEventsHandler.ReportingCheater))]
[HarmonyPatch(typeof(CheaterReport), @"UserCode_CmdReport__UInt32__String__Byte[]__Boolean")]
public class ReportingPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<ReportingPatch>(362, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder localReportArgs = generator.DeclareLocal(typeof(LocalReportingEventArgs));
        LocalBuilder cheaterReportArgs = generator.DeclareLocal(typeof(ReportingCheaterEventArgs));

        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_I4_7);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldfld, AccessTools.Field(typeof(CheaterReport), nameof(CheaterReport._hub))),
            new (OpCodes.Ldloc_2),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(LocalReportingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, localReportArgs.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedBanSystemEventsHandler), nameof(CursedBanSystemEventsHandler.OnLocalReporting))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(LocalReportingEventArgs), nameof(LocalReportingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, localReportArgs.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(LocalReportingEventArgs), nameof(LocalReportingEventArgs.Reason))),
            new (OpCodes.Starg_S, 2),
        });

        index = newInstructions.FindLastIndex(i => i.StoresField(AccessTools.Field(typeof(CheaterReport), nameof(CheaterReport._lastReport)))) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldfld, AccessTools.Field(typeof(CheaterReport), nameof(CheaterReport._hub))),
            new (OpCodes.Ldloc_2),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(ReportingCheaterEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, cheaterReportArgs.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedBanSystemEventsHandler), nameof(CursedBanSystemEventsHandler.OnReportingCheater))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ReportingCheaterEventArgs), nameof(ReportingCheaterEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, cheaterReportArgs.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ReportingCheaterEventArgs), nameof(ReportingCheaterEventArgs.Reason))),
            new (OpCodes.Starg_S, 2),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}