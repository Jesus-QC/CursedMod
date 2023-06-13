// -----------------------------------------------------------------------
// <copyright file="IssuingBanPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedBanSystemEventsHandler), nameof(CursedBanSystemEventsHandler.IssuingBan))]
[HarmonyPatch(typeof(BanHandler), nameof(BanHandler.IssueBan))]
public class IssuingBanPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<IssuingBanPatch>(134, instructions);

        Label skip = generator.DefineLabel();
        LocalBuilder builder = generator.DeclareLocal(typeof(IssuingBanEventArgs));
        
        // TODO: This is a temporary event until I have time to implement it correctly
        newInstructions.InsertRange(0, new[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(IssuingBanEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedBanSystemEventsHandler), nameof(CursedBanSystemEventsHandler.OnIssuingBan))),
            new (OpCodes.Stloc_S, builder.LocalIndex),
            new (OpCodes.Ldloc_S, builder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(IssuingBanEventArgs), nameof(IssuingBanEventArgs.IsAllowed))),
            new (OpCodes.Brtrue_S, skip),
            new (OpCodes.Ldc_I4_1),
            new (OpCodes.Ret),
            new CodeInstruction(OpCodes.Ldloc_S, builder.LocalIndex).WithLabels(skip),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(IssuingBanEventArgs), nameof(IssuingBanEventArgs.BanDetails))),
            new (OpCodes.Starg_S, 0),
            new (OpCodes.Ldloc_S, builder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(IssuingBanEventArgs), nameof(IssuingBanEventArgs.BanType))),
            new (OpCodes.Starg_S, 1),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}