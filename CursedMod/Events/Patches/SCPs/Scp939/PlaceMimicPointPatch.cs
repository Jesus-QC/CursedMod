// -----------------------------------------------------------------------
// <copyright file="PlaceMimicPointPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp939;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using RelativePositioning;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlacingMimicPoint))]
[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.RemovingMimic))]
[HarmonyPatch(typeof(MimicPointController), nameof(MimicPointController.ServerProcessCmd))]
public class PlaceMimicPointPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<PlaceMimicPointPatch>(30, instructions);
        
        Label returnLabel = generator.DefineLabel();

        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        newInstructions.InsertRange(3, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.Method(typeof(PlaceMimicPointPatch), nameof(ProcessMimicPointEvents))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool ProcessMimicPointEvents(MimicPointController controller)
    {
        if (controller.Active)
        {
            Scp939RemovingMimicPointEventArgs args = new (controller);
            CursedScp939EventsHandler.OnRemovingMimicPoint(args);
            return args.IsAllowed;
        }
        else
        {
            Scp939PlacingMimicPointEventArgs args = new (controller);
            CursedScp939EventsHandler.OnPlacingMimicPoint(args);
            return args.IsAllowed;
        }
    }
}