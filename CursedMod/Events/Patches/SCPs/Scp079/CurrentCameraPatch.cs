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
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.ChangingCamera))]
[HarmonyPatch(typeof(Scp079CurrentCameraSync), nameof(Scp079CurrentCameraSync.ServerProcessCmd))]
public class CurrentCameraPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<CurrentCameraPatch>(117, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Conv_R4) + 2;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.Method(typeof(CurrentCameraPatch), nameof(ProcessChangeCameraEvent))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
    
    private static bool ProcessChangeCameraEvent(Scp079CurrentCameraSync currentCameraSync)
    {
        Scp079ChangingCameraEventArgs args = new (currentCameraSync);
        CursedScp079EventsHandler.OnChangingCamera(args);
        return args.IsAllowed;
    }
}