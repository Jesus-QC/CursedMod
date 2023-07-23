// -----------------------------------------------------------------------
// <copyright file="ToggleNoClipPatch.cs" company="CursedMod">
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
using PlayerRoles.FirstPersonControl.NetworkMessages;

namespace CursedMod.Events.Patches.Player;

[DynamicEventPatch(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.TogglingNoClip))]
[HarmonyPatch(typeof(FpcNoclipToggleMessage), nameof(FpcNoclipToggleMessage.ProcessMessage))]
public class ToggleNoClipPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<ToggleNoClipPatch>(28, instructions);

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);

        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerTogglingNoClipEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerTogglingNoClip))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerTogglingNoClipEventArgs), nameof(PlayerTogglingNoClipEventArgs.IsAllowed))),
            new (OpCodes.Brfalse, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}