// -----------------------------------------------------------------------
// <copyright file="CurrentCameraPatch.cs" company="CursedMod">
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
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(Scp079EventsHandler), nameof(Scp079EventsHandler.PlayerChangeCamera))]
[HarmonyPatch(typeof(Scp079CurrentCameraSync), nameof(Scp079CurrentCameraSync.ServerProcessCmd))]
public class CurrentCameraPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<CurrentCameraPatch>(125, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        const int offset = 2;
        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Conv_R4) + offset;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloca_S, 0),
            new (OpCodes.Call, AccessTools.Method(typeof(CurrentCameraPatch), nameof(ProcessChangeCameraEvent))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
    
    private static bool ProcessChangeCameraEvent(Scp079CurrentCameraSync currentCameraSync, ref float switchCost)
    {
        PlayerChangeCameraEventArgs args = new (currentCameraSync, (int)switchCost);
        Scp079EventsHandler.OnPlayerChangeCamera(args);
        
        currentCameraSync.ServerSendRpc(true);
        
        if (args.IsAllowed)
            switchCost = args.PowerCost;

        return args.IsAllowed;
    }
}