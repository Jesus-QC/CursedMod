// -----------------------------------------------------------------------
// <copyright file="ElevatorStateChangerPatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.MovingElevator))]
[HarmonyPatch(typeof(Scp079ElevatorStateChanger), nameof(Scp079ElevatorStateChanger.ServerProcessCmd))]
public class ElevatorStateChangerPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<ElevatorStateChangerPatch>(104, instructions);
        
        Label returnLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(Scp079MovingElevatorEventArgs));
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_S) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_3),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp079MovingElevatorEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnMovingElevator))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079MovingElevatorEventArgs), nameof(Scp079MovingElevatorEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}