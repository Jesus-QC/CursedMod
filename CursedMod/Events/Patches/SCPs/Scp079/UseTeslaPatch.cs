// -----------------------------------------------------------------------
// <copyright file="UseTeslaPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp079;
using CursedMod.Events.Handlers;
using CursedMod.Features.Wrappers.Facility.Props;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Rewards;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.PlayerUseTesla))]
[HarmonyPatch(typeof(Scp079TeslaAbility), nameof(Scp079TeslaAbility.ServerProcessCmd))]
public class UseTeslaPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<UseTeslaPatch>(66, instructions);
        
        Label returnLabel = generator.DefineLabel();

        const int offset = -1;
        int index = newInstructions.FindIndex(i =>
            i.Calls(AccessTools.Method(typeof(Scp079RewardManager), nameof(Scp079RewardManager.MarkRoom)))) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedTeslaGate), nameof(CursedTeslaGate.Get), new[] { typeof(TeslaGate) })),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUseTeslaEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnPlayerUseTesla))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUseTeslaEventArgs), nameof(PlayerUseTeslaEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}