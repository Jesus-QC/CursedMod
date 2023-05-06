// -----------------------------------------------------------------------
// <copyright file="StalkAbilityPatch.cs" company="CursedMod">
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
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Events.Patches.SCPs.Scp106;

[DynamicEventPatch(typeof(Scp106EventsHandler), nameof(Scp106EventsHandler.PlayerStalking))]
[HarmonyPatch(typeof(Scp106StalkAbility), nameof(Scp106StalkAbility.ServerProcessCmd))]
public class StalkAbilityPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<StalkAbilityPatch>(46, instructions);
        
        Label returnLabel = generator.DefineLabel();

        const int offset = -1;
        int index = newInstructions.FindIndex(i =>
                        i.Calls(AccessTools.PropertyGetter(typeof(Scp106StalkAbility), nameof(Scp106StalkAbility.IsActive)))) + offset;

        newInstructions.InsertRange(45, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerStalkingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp106EventsHandler), nameof(Scp106EventsHandler.OnPlayerStalking))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerStalkingEventArgs), nameof(PlayerStalkingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}