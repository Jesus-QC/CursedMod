// -----------------------------------------------------------------------
// <copyright file="GeneratorInteractPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Generators;
using CursedMod.Events.Handlers;
using HarmonyLib;
using MapGeneration.Distributors;
using NorthwoodLib.Pools;
using PluginAPI.Events;

namespace CursedMod.Events.Patches.Facility.Generators;

[DynamicEventPatch(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.ClosingGenerator))]
[DynamicEventPatch(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OpeningGenerator))]
[DynamicEventPatch(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.UnlockingGenerator))]
[DynamicEventPatch(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.ActivatingGenerator))]
[DynamicEventPatch(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.DeactivatingGenerator))]
[HarmonyPatch(typeof(Scp079Generator), nameof(Scp079Generator.ServerInteract))]
public class GeneratorInteractPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<GeneratorInteractPatch>(207, instructions);
        
        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerCloseGeneratorEvent))[0])) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerClosingGeneratorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OnClosingGenerator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerClosingGeneratorEventArgs), nameof(PlayerClosingGeneratorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerOpenGeneratorEvent))[0])) + 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerOpeningGeneratorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OnOpeningGenerator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerOpeningGeneratorEventArgs), nameof(PlayerOpeningGeneratorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerUnlockGeneratorEvent))[0])) + 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUnlockingGeneratorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OnUnlockingGenerator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUnlockingGeneratorEventArgs), nameof(PlayerUnlockingGeneratorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerActivateGeneratorEvent))[0])) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerActivatingGeneratorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OnActivatingGenerator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerActivatingGeneratorEventArgs), nameof(PlayerActivatingGeneratorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerDeactivatedGeneratorEvent))[0])) + 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerDeactivatingGeneratorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OnDeactivatingGenerator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerDeactivatingGeneratorEventArgs), nameof(PlayerDeactivatingGeneratorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindLastIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerDeactivatedGeneratorEvent))[0])) + 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerDeactivatingGeneratorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedGeneratorEventHandler), nameof(CursedGeneratorEventHandler.OnDeactivatingGenerator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerDeactivatingGeneratorEventArgs), nameof(PlayerDeactivatingGeneratorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}