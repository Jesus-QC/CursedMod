﻿// -----------------------------------------------------------------------
// <copyright file="EnragingPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp096;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Patches.SCPs.Scp096;

[DynamicEventPatch(typeof(CursedScp096EventsHandler), nameof(CursedScp096EventsHandler.Enraging))]
[HarmonyPatch(typeof(Scp096RageManager), nameof(Scp096RageManager.ServerEnrage))]
public class EnragingPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<EnragingPatch>(37, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(Scp096EnragingEventArgs));

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Pop) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp096EnragingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp096EventsHandler), nameof(CursedScp096EventsHandler.OnEnraging))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp096EnragingEventArgs), nameof(Scp096EnragingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp096EnragingEventArgs), nameof(Scp096EnragingEventArgs.RageTime))),
            new (OpCodes.Starg_S, 1),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}