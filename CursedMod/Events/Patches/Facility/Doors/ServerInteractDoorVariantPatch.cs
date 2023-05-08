// -----------------------------------------------------------------------
// <copyright file="ServerInteractDoorVariantPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Facility.Doors;
using CursedMod.Events.Handlers.Facility.Doors;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Facility.Doors;

[DynamicEventPatch(typeof(DoorsEventsHandler), nameof(DoorsEventsHandler.PlayerInteractingDoor))]
[HarmonyPatch(typeof(DoorVariant), nameof(DoorVariant.ServerInteract))]
public class ServerInteractDoorVariantPatch
{
    // Todo: Review transpiler after 13.0 to check changes
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerInteractDoorVariantPatch>(135, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(PlayerInteractingDoorEventArgs));
        Label ret = generator.DefineLabel();
            
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Beq_S) + 11;
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldloc_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerInteractingDoorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(DoorsEventsHandler), nameof(DoorsEventsHandler.OnPlayerInteractingDoor))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerInteractingDoorEventArgs), nameof(PlayerInteractingDoorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerInteractingDoorEventArgs), nameof(PlayerInteractingDoorEventArgs.HasPermissions))),
            new (OpCodes.Stloc_0),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}