// -----------------------------------------------------------------------
// <copyright file="CallingZombiesPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp049;
using CursedMod.Events.Handlers.SCPs.Scp049;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Events.Patches.SCPs.Scp049;

[DynamicEventPatch(typeof(Scp049EventsHandler), nameof(Scp049EventsHandler.PlayerCalling))]
[HarmonyPatch(typeof(Scp049CallAbility), nameof(Scp049CallAbility.ServerProcessCmd))]
public class CallingZombiesPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<CallingZombiesPatch>(19, instructions);
        
        Label returnLabel = generator.DefineLabel();

        const int offset = 1;
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerCallingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp049EventsHandler), nameof(Scp049EventsHandler.OnPlayerCalling))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerCallingEventArgs), nameof(PlayerCallingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[index - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}