// -----------------------------------------------------------------------
// <copyright file="PlayerEscapePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Player.EscapeSystem;

[DynamicEventPatch(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.Escaping))]
[HarmonyPatch(typeof(Escape), nameof(Escape.ServerHandlePlayer))]
public class PlayerEscapePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<PlayerEscapePatch>(61, instructions);

        Label ret = generator.DefineLabel();
        Label ev = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(PlayerEscapingEventArgs));
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret);
        
        newInstructions[offset].opcode = OpCodes.Br_S;
        newInstructions[offset].operand = ev;
            
        offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldloc_0) - 1;

        newInstructions.InsertRange(offset, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[offset]).WithLabels(ev),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerEscapingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerEscaping))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEscapingEventArgs), nameof(PlayerEscapingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEscapingEventArgs), nameof(PlayerEscapingEventArgs.NewRole))),
            new (OpCodes.Stloc_0, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEscapingEventArgs), nameof(PlayerEscapingEventArgs.EscapeScenarioType))),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_1, ret),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}