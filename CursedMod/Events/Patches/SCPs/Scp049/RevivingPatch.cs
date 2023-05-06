// -----------------------------------------------------------------------
// <copyright file="RevivingPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(Scp049EventsHandler), nameof(Scp049EventsHandler.PlayerReviving))]
[HarmonyPatch(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.ServerValidateBegin))]
public class RevivingPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RevivingPatch>(16, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerRevivingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp049EventsHandler), nameof(Scp049EventsHandler.OnPlayerReviving))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerRevivingEventArgs), nameof(PlayerRevivingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (var instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}