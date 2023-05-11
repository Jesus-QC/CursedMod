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
using CursedMod.Events.Handlers.SCPs.Scp079;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.PlayerUseLockdown))]
[HarmonyPatch(typeof(Scp079LockdownRoomAbility), nameof(Scp079LockdownRoomAbility.ServerProcessCmd))]
public class LockdownActivatePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<LockdownActivatePatch>(50, instructions);
        
        Label returnLabel = generator.DefineLabel();
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_S);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.PropertyGetter(typeof(Scp079AbilityBase), nameof(Scp079AbilityBase.CurrentCamSync))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079CurrentCameraSync), nameof(Scp079CurrentCameraSync.CurrentCamera))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079Camera), nameof(Scp079Camera.Room))),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUseLockdownEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.OnPlayerUseLockdown))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUseLockdownEventArgs), nameof(PlayerUseLockdownEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}