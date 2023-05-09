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
using CursedMod.Events.Handlers.SCPs.Scp096;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Events.Patches.SCPs.Scp096;

[DynamicEventPatch(typeof(Scp096EventsHandler), nameof(Scp096EventsHandler.PlayerEndEnrage))]
[HarmonyPatch(typeof(Scp096RageManager), nameof(Scp096RageManager.ServerEndEnrage))]
public class EndEnragePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<EndEnragePatch>(17, instructions);

        Label retLabel = generator.DefineLabel();
        LocalBuilder localBuilder = generator.DeclareLocal(typeof(PlayerEndEnrageEventArgs));
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerEndEnrageEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, localBuilder.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp096EventsHandler), nameof(Scp096EventsHandler.OnPlayerEndEnrage))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEndEnrageEventArgs), nameof(PlayerEndEnrageEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, localBuilder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerEndEnrageEventArgs), nameof(PlayerEndEnrageEventArgs.ClearTime))),
            new (OpCodes.Starg_S, 1),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}