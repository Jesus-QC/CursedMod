// -----------------------------------------------------------------------
// <copyright file="ServerCancelLockdownPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp079;
using CursedMod.Events.Handlers.SCPs.Scp079;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.PlayerCancelLockdown))]
[HarmonyPatch(typeof(Scp079LockdownRoomAbility), nameof(Scp079LockdownRoomAbility.ServerCancelLockdown))]
public class ServerCancelLockdownPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerCancelLockdownPatch>(60, instructions);

        Label returnLabel = generator.DefineLabel();
        const int offset = 1;
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerCancelLockdownEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.OnPlayerCancelLockdown))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerCancelLockdownEventArgs), nameof(PlayerCancelLockdownEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}