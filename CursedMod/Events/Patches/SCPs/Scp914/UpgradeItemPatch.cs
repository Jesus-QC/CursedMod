// -----------------------------------------------------------------------
// <copyright file="UpgradeItemPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp914;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using Scp914;

namespace CursedMod.Events.Patches.SCPs.Scp914;

[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.UpgradingItem))]
[HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPickup))]
public class UpgradeItemPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<UpgradeItemPatch>(78, instructions);

        LocalBuilder localBuilder = generator.DeclareLocal(typeof(Scp914UpgradingItemEventArgs));
        Label returnLabel = generator.DefineLabel();
        
        const int offset = 1;
        int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Stloc_1) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp914UpgradingItemEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, localBuilder.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.OnUpgradingItem))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp914UpgradingItemEventArgs), nameof(Scp914UpgradingItemEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldloc_S, localBuilder.LocalIndex),
            new (OpCodes.Dup),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp914UpgradingItemEventArgs), nameof(Scp914UpgradingItemEventArgs.OutputPosition))),
            new (OpCodes.Stloc_1),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp914UpgradingItemEventArgs), nameof(Scp914UpgradingItemEventArgs.KnobSetting))),
            new (OpCodes.Starg_S, 3),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}