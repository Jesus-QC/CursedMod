// -----------------------------------------------------------------------
// <copyright file="ElevatorInteractPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Elevators;
using CursedMod.Events.Handlers;
using HarmonyLib;
using Interactables.Interobjects;
using NorthwoodLib.Pools;
using PluginAPI.Events;

namespace CursedMod.Events.Patches.Facility.Elevators;

[DynamicEventPatch(typeof(CursedElevatorEventHandler), nameof(CursedElevatorEventHandler.PlayerInteractingElevator))]
[HarmonyPatch(typeof(ElevatorManager), nameof(ElevatorManager.ServerReceiveMessage))]
public class ElevatorInteractPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<ElevatorInteractPatch>(77, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder elevatorInteractArgs = generator.DeclareLocal(typeof(PlayerInteractingElevatorEventArgs));
        
        int index = newInstructions.FindLastIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerInteractElevatorEvent))[0])) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldloc_3),
            new (OpCodes.Ldloc_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerInteractingElevatorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, elevatorInteractArgs.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedElevatorEventHandler), nameof(CursedElevatorEventHandler.OnPlayerInteractingElevator))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerInteractingElevatorEventArgs), nameof(PlayerInteractingElevatorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, elevatorInteractArgs.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerInteractingElevatorEventArgs), nameof(PlayerInteractingElevatorEventArgs.TargetLevel))),
            new (OpCodes.Starg_S, 2),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}