﻿// -----------------------------------------------------------------------
// <copyright file="StalkAbilityPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp106;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Events.Patches.SCPs.Scp106;

[DynamicEventPatch(typeof(CursedScp106EventsHandler), nameof(CursedScp106EventsHandler.UsingStalkAbility))]
[HarmonyPatch(typeof(Scp106StalkAbility), nameof(Scp106StalkAbility.ServerProcessCmd))]
public class StalkAbilityPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<StalkAbilityPatch>(46, instructions);
        
        Label ret = generator.DefineLabel();
        newInstructions[newInstructions.Count - 1].labels.Add(ret);

        int index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Ldarg_0);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp106UsingStalkAbilityEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp106EventsHandler), nameof(CursedScp106EventsHandler.OnUsingStalkAbility))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp106UsingStalkAbilityEventArgs), nameof(Scp106UsingStalkAbilityEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}