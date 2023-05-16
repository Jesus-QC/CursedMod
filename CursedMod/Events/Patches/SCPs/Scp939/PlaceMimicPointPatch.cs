﻿// -----------------------------------------------------------------------
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

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlayerPlaceMimicPoint))]
[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlayerRemoveMimicPoint))]
[HarmonyPatch(typeof(MimicPointController), nameof(MimicPointController.ServerProcessCmd))]
public class PlaceMimicPointPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<PlaceMimicPointPatch>(30, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.Method(typeof(PlaceMimicPointPatch), nameof(ProcessMimicPointEvents))),
            new (OpCodes.Br, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ProcessMimicPointEvents(MimicPointController controller)
    {
        if (controller.Active)
        {
            PlayerRemoveMimicPointEventArgs args = new (controller);
            CursedScp939EventsHandler.OnPlayerRemoveMimicPoint(args);
            
            if (!args.IsAllowed)
                return;
            
            controller._syncMessage = MimicPointController.RpcStateMsg.RemovedByUser;
            controller.Active = false;
        }
        else
        {
            PlayerPlaceMimicPointEventArgs args = new (controller);
            CursedScp939EventsHandler.OnPlayerPlaceMimicPoint(args);

            if (!args.IsAllowed)
                return;
            
            controller._syncMessage = MimicPointController.RpcStateMsg.PlacedByUser;
            controller._syncPos = new RelativePosition(controller.ScpRole.FpcModule.Position);
            controller.Active = true;
        }
        
        controller.ServerSendRpc(true);
    }
}