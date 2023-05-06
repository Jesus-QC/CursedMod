// -----------------------------------------------------------------------
// <copyright file="TriggerBreakNeckSpeedPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp173;
using CursedMod.Events.Handlers.SCPs.Scp173;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;

namespace CursedMod.Events.Patches.SCPs.Scp173;

[DynamicEventPatch(typeof(Scp173EventsHandler), nameof(Scp173EventsHandler.PlayerUseBreakneckSpeed))]
[HarmonyPatch(typeof(Scp173BreakneckSpeedsAbility), nameof(Scp173BreakneckSpeedsAbility.ServerProcessCmd))]
public class TriggerBreakNeckSpeedPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<TriggerBreakNeckSpeedPatch>(21, instructions);

        LocalBuilder localBuilder = generator.DeclareLocal(typeof(PlayerUseBreakneckSpeedEventArgs));
        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUseBreakneckSpeedEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, localBuilder.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp173EventsHandler), nameof(Scp173EventsHandler.OnPlayerUseBreakneckSpeed))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUseBreakneckSpeedEventArgs), nameof(PlayerUseBreakneckSpeedEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}