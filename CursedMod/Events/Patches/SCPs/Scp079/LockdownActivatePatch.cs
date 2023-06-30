// -----------------------------------------------------------------------
// <copyright file="LockdownActivatePatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.UsingLockdownAbility))]
[HarmonyPatch(typeof(Scp079LockdownRoomAbility), nameof(Scp079LockdownRoomAbility.ServerProcessCmd))]
public class LockdownActivatePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<LockdownActivatePatch>(42, instructions);
        
        Label returnLabel = generator.DefineLabel();
        LocalBuilder localBuilder = generator.DeclareLocal(typeof(Scp079UsingLockdownAbilityEventArgs));
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Callvirt) + 2;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp079UsingLockdownAbilityEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnUsingLockdownAbility))),
            new (OpCodes.Stloc_S, localBuilder.LocalIndex),
            new (OpCodes.Ldloc_S, localBuilder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079UsingLockdownAbilityEventArgs), nameof(Scp079UsingLockdownAbilityEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloc_S, localBuilder.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079UsingLockdownAbilityEventArgs), nameof(Scp079UsingLockdownAbilityEventArgs.Duration))),
            new (OpCodes.Stfld, AccessTools.Field(typeof(Scp079LockdownRoomAbility), nameof(Scp079LockdownRoomAbility._lockdownDuration))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}