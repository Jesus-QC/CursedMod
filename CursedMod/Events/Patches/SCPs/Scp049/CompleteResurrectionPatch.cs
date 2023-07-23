// -----------------------------------------------------------------------
// <copyright file="CompleteResurrectionPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp049;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Patches.SCPs.Scp049;

[DynamicEventPatch(typeof(CursedScp049EventsHandler), nameof(CursedScp049EventsHandler.ResurrectingPlayer))]
[HarmonyPatch(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.ServerComplete))]
public class CompleteResurrectionPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<CompleteResurrectionPatch>(51, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(newInstructions.FindIndex(x => x.opcode == OpCodes.Stloc_0) + 1, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp049ResurrectingPlayerEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp049EventsHandler), nameof(CursedScp049EventsHandler.OnResurrectingPlayer))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp049ResurrectingPlayerEventArgs), nameof(Scp049ResurrectingPlayerEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}