// -----------------------------------------------------------------------
// <copyright file="EndEnragePatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedScp096EventsHandler), nameof(CursedScp096EventsHandler.Calming))]
[HarmonyPatch(typeof(Scp096RageManager), nameof(Scp096RageManager.ServerEndEnrage))]
public class EndEnragePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<EndEnragePatch>(17, instructions);

        Label retLabel = generator.DefineLabel();
        LocalBuilder localBuilder = generator.DeclareLocal(typeof(Scp096CalmingEventArgs));
        
        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp096CalmingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp096EventsHandler), nameof(CursedScp096EventsHandler.OnCalming))),
            new (OpCodes.Stloc_S, localBuilder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp096CalmingEventArgs), nameof(Scp096CalmingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, localBuilder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp096CalmingEventArgs), nameof(Scp096CalmingEventArgs.ClearTime))),
            new (OpCodes.Starg_S, 1),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}