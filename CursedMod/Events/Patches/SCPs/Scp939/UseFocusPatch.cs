// -----------------------------------------------------------------------
// <copyright file="UseFocusPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp939;
using CursedMod.Events.Handlers.SCPs.Scp939;
using HarmonyLib;
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(Scp939EventsHandler), nameof(Scp939EventsHandler.PlayerUseFocus))]
[HarmonyPatch(typeof(Scp939FocusKeySync), nameof(Scp939FocusKeySync.ServerProcessCmd))]
public class UseFocusPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<UseFocusPatch>(8, instructions);

        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(UseFocusPatch), nameof(ProcessFocusEvent))),
            new (OpCodes.Br, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ProcessFocusEvent(Scp939FocusKeySync focusKeySync, NetworkReader reader)
    {
        var focusState = reader.ReadBool();

        PlayerUseFocusEventArgs args = new (focusKeySync, focusState);
        Scp939EventsHandler.OnPlayerUseFocus(args);

        if (!args.IsAllowed)
            return;

        focusKeySync.FocusKeyHeld = focusState;
    }
}
