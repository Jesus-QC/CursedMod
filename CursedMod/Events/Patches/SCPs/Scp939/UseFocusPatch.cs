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
using CursedMod.Events.Handlers;
using HarmonyLib;
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.UsingFocusAbility))]
[HarmonyPatch(typeof(Scp939FocusKeySync), nameof(Scp939FocusKeySync.ServerProcessCmd))]
public class UseFocusPatch
{
    // TODO: REVIEW
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
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ProcessFocusEvent(Scp939FocusKeySync focusKeySync, NetworkReader reader)
    {
        bool focusState = reader.ReadBool();

        Scp939UsingFocusAbilityEventArgs args = new (focusKeySync, focusState);
        CursedScp939EventsHandler.OnUsingFocusAbility(args);

        if (!args.IsAllowed)
            return;

        focusKeySync.FocusKeyHeld = focusState;
    }
}