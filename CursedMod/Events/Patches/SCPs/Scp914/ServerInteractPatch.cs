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

[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.PlayerChangeKnobSetting))]
[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.PlayerStart))]
[HarmonyPatch(typeof(Scp914Controller), nameof(Scp914Controller.ServerInteract))]
public class ServerInteractPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerInteractPatch>(89, instructions);
        
        Label returnLabel = generator.DefineLabel();

        const int offset = -2;
        int index = newInstructions.FindIndex(i =>
            i.Calls(AccessTools.PropertySetter(typeof(Scp914Controller), nameof(Scp914Controller.Network_knobSetting)))) + offset;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerChangeKnobSettingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.OnPlayerChangeKnobSetting))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerChangeKnobSettingEventArgs), nameof(PlayerChangeKnobSettingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.LoadsField(AccessTools.Field(typeof(Scp914Controller), nameof(Scp914Controller._totalSequenceTime)))) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerStart914EventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.OnPlayerStart914))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerStart914EventArgs), nameof(PlayerStart914EventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}