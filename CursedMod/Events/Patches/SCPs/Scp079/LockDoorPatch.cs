// -----------------------------------------------------------------------
// <copyright file="LockDoorPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp079;
using CursedMod.Events.Handlers;
using CursedMod.Features.Wrappers.Facility.Doors;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.ChangingDoorLock))]
[HarmonyPatch(typeof(Scp079DoorLockChanger), nameof(Scp079DoorLockChanger.SetDoorLock))]
public class LockDoorPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<LockDoorPatch>(35, instructions);
        
        Label returnLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(Scp079ChangingDoorLockEventArgs));
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp079ChangingDoorLockEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnChangingDoorLock))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079ChangingDoorLockEventArgs), nameof(Scp079ChangingDoorLockEventArgs.IsAllowed))),
            new (OpCodes.Brfalse, returnLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079ChangingDoorLockEventArgs), nameof(Scp079ChangingDoorLockEventArgs.Door))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(CursedDoor), nameof(CursedDoor.Base))),
            new (OpCodes.Starg_S, 1),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079ChangingDoorLockEventArgs), nameof(Scp079ChangingDoorLockEventArgs.NewLockState))),
            new (OpCodes.Starg_S, 2),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079ChangingDoorLockEventArgs), nameof(Scp079ChangingDoorLockEventArgs.SkipChecks))),
            new (OpCodes.Starg_S, 3),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}