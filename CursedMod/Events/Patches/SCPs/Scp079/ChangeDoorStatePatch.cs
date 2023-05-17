// -----------------------------------------------------------------------
// <copyright file="ChangeDoorStatePatch.cs" company="CursedMod">
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
using Interactables.Interobjects.DoorUtils;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.PlayerChangeDoorState))]
[HarmonyPatch(typeof(Scp079DoorStateChanger), nameof(Scp079DoorStateChanger.ServerProcessCmd))]
public class ChangeDoorStatePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ChangeDoorStatePatch>(75, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        const int offset = -2;
        int index = newInstructions.FindIndex(
            i => i.LoadsField(AccessTools.Field(typeof(DoorVariant), nameof(DoorVariant.TargetState)))) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldfld, AccessTools.Field(typeof(Scp079DoorStateChanger), nameof(Scp079DoorStateChanger.LastDoor))),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerChangeDoorStateEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnPlayerChangeDoorState))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerChangeDoorStateEventArgs), nameof(PlayerChangeDoorStateEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}