// -----------------------------------------------------------------------
// <copyright file="TogglingBreakNeckSpeedPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp173;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;

namespace CursedMod.Events.Patches.SCPs.Scp173;

[DynamicEventPatch(typeof(CursedScp173EventsHandler), nameof(CursedScp173EventsHandler.TogglingBreakneckSpeedAbility))]
[HarmonyPatch(typeof(Scp173BreakneckSpeedsAbility), nameof(Scp173BreakneckSpeedsAbility.ServerProcessCmd))]
public class TogglingBreakNeckSpeedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<TogglingBreakNeckSpeedPatch>(21, instructions);
        
        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp173TogglingBreakneckSpeedAbilityEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp173EventsHandler), nameof(CursedScp173EventsHandler.OnTogglingBreakneckSpeedAbility))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp173TogglingBreakneckSpeedAbilityEventArgs), nameof(Scp173TogglingBreakneckSpeedAbilityEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}