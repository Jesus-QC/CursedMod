﻿// -----------------------------------------------------------------------
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
    // TODO: REVIEW
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<BlackoutZoneAbilityPatch>(71, instructions);
        
        const int offset = 4;
        int index = newInstructions.FindIndex(i => i.LoadsField(AccessTools.Field(typeof(Scp079BlackoutZoneAbility), nameof(Scp079BlackoutZoneAbility._syncZone)))) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Pop),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.Method(typeof(BlackoutZoneAbilityPatch), nameof(ProcessBlackoutZoneEvent))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool ProcessBlackoutZoneEvent(Scp079BlackoutZoneAbility blackoutZoneAbility)
    {
        Scp079UsingBlackoutZoneAbilityEventArgs args = new (blackoutZoneAbility, blackoutZoneAbility._cost, blackoutZoneAbility._duration);
        CursedScp079EventsHandler.OnUsingBlackoutZoneAbility(args);

        if (!args.IsAllowed)
            return false;
        
        blackoutZoneAbility._duration = args.Duration;
        blackoutZoneAbility._cooldownTimer.Trigger(blackoutZoneAbility._cooldown);
        blackoutZoneAbility._cost = args.PowerCost;
        return true;
    }
}