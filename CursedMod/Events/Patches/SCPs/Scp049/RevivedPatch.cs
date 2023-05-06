// -----------------------------------------------------------------------
// <copyright file="RevivedPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp049;
using CursedMod.Events.Handlers.SCPs.Scp049;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Patches.SCPs.Scp049;

[DynamicEventPatch(typeof(Scp049EventsHandler), nameof(Scp049EventsHandler.PlayerFinishRevive))]
[HarmonyPatch(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.ServerComplete))]
public class RevivedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RevivedPatch>(62, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(5, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerFinishReviveEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp049EventsHandler), nameof(Scp049EventsHandler.OnPlayerRevived))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerFinishReviveEventArgs), nameof(PlayerFinishReviveEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}