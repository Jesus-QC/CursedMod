// -----------------------------------------------------------------------
// <copyright file="HuntersAtlasPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp106;
using CursedMod.Events.Handlers.SCPs.Scp106;
using HarmonyLib;
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Events.Patches.SCPs.Scp106;

[DynamicEventPatch(typeof(Scp106EventsHandler), nameof(Scp106EventsHandler.PlayerSubmerging))]
[HarmonyPatch(typeof(Scp106HuntersAtlasAbility), nameof(Scp106HuntersAtlasAbility.SetSubmerged), typeof(bool))]
public class HuntersAtlasPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<HuntersAtlasPatch>(25, instructions);
        
        Label returnLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(HuntersAtlasPatch), nameof(ProcessSubmerging))),
            new (OpCodes.Br, returnLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (var instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ProcessSubmerging(Scp106HuntersAtlasAbility atlasAbility, bool val)
    {
        if (atlasAbility._submerged == val)
            return;
        atlasAbility._submerged = val;

        if (val)
        {
            PlayerSubmergingEventArgs args = new (atlasAbility);
            Scp106EventsHandler.OnPlayerStartSubmerging(args);

            if (!args.IsAllowed)
                return;
            
            atlasAbility._dissolveAnim = true;
            atlasAbility.ScpRole.Sinkhole.TargetDuration = 2.5f;
        }
        
        PlayerExitingSubmergeEventArgs args2 = new (atlasAbility);
        Scp106EventsHandler.OnPlayerExitingSubmerge(args2);
            
        if (!NetworkServer.active)
            return;
        atlasAbility.ServerSendRpc(true);
    }
}
