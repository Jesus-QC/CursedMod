// -----------------------------------------------------------------------
// <copyright file="BlackoutZoneAbilityPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp079;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.UsingBlackoutZoneAbility))]
[HarmonyPatch(typeof(Scp079BlackoutZoneAbility), nameof(Scp079BlackoutZoneAbility.ServerProcessCmd))]
public class BlackoutZoneAbilityPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<BlackoutZoneAbilityPatch>(71, instructions);

        Label ret = generator.DefineLabel();
        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Call, AccessTools.Method(typeof(BlackoutZoneAbilityPatch), nameof(ProcessBlackoutZoneEvent))),
            new (OpCodes.Brfalse_S, ret),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool ProcessBlackoutZoneEvent(Scp079BlackoutZoneAbility blackoutZoneAbility)
    {
        Scp079UsingBlackoutZoneAbilityEventArgs args = new (blackoutZoneAbility);
        CursedScp079EventsHandler.OnUsingBlackoutZoneAbility(args);

        if (!args.IsAllowed)
            return false;
        
        blackoutZoneAbility._duration = args.Duration;
        blackoutZoneAbility._syncZone = args.Zone;
        return true;
    }
}