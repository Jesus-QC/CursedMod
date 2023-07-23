// -----------------------------------------------------------------------
// <copyright file="ServerInteractPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.PlayerChangingScp914KnobSetting))]
[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.PlayerEnablingScp914))]
[HarmonyPatch(typeof(Scp914Controller), nameof(Scp914Controller.ServerInteract))]
public class ServerInteractPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<ServerInteractPatch>(72, instructions);
        
        Label returnLabel = generator.DefineLabel();
        LocalBuilder changingKnobArgs = generator.DeclareLocal(typeof(PlayerChangingScp914KnobSettingEventArgs));
        
        int index = newInstructions.FindIndex(i => i.Calls(AccessTools.PropertySetter(typeof(Scp914Controller), nameof(Scp914Controller.Network_knobSetting)))) - 2;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerChangingScp914KnobSettingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.OnPlayerChangingScp914KnobSetting))),
            new (OpCodes.Stloc_S, changingKnobArgs.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerChangingScp914KnobSettingEventArgs), nameof(PlayerChangingScp914KnobSettingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldloc_S, changingKnobArgs.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerChangingScp914KnobSettingEventArgs), nameof(PlayerChangingScp914KnobSettingEventArgs.KnobSetting))),
            new (OpCodes.Stloc_1),
        });

        index = newInstructions.FindIndex(x => x.opcode == OpCodes.Beq_S) + 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerEnablingScp914EventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.OnPlayerEnablingScp914))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEnablingScp914EventArgs), nameof(PlayerEnablingScp914EventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
