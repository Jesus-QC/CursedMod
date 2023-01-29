// -----------------------------------------------------------------------
// <copyright file="PlayerDisconnectPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Player;

[HarmonyPatch(typeof(CustomNetworkManager), nameof(CustomNetworkManager.OnServerDisconnect))]
public class PlayerDisconnectPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<PlayerDisconnectPatch>(45, instructions);

        newInstructions.InsertRange(newInstructions.FindIndex(x => x.opcode == OpCodes.Pop) + 1, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerDisconnectedEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerEventHandlers), nameof(PlayerEventHandlers.OnPlayerDisconnected))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}