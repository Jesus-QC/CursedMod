// -----------------------------------------------------------------------
// <copyright file="CompleteVerificationPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Player;

[HarmonyPatch(typeof(ServerRoles), nameof(ServerRoles.UserCode_CmdServerSignatureComplete__String__String__String__Boolean))]
public class CompleteVerificationPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<CompleteVerificationPatch>(707, instructions);

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(newInstructions.FindIndex(x => x.opcode == OpCodes.Pop) + 1, new List<CodeInstruction>()
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ServerRoles), nameof(ServerRoles.isLocalPlayer))),
            new (OpCodes.Brtrue_S, ret),
            
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerConnectedEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerConnected))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}