// -----------------------------------------------------------------------
// <copyright file="PlaceTantrumPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp173;
using CursedMod.Events.Handlers.SCPs.Scp173;
using HarmonyLib;
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using UnityEngine;

namespace CursedMod.Events.Patches.SCPs.Scp173;

[DynamicEventPatch(typeof(Scp173EventsHandler), nameof(Scp173EventsHandler.PlayerPlaceTantrum))]
[HarmonyPatch(typeof(Scp173TantrumAbility), nameof(Scp173TantrumAbility.ServerProcessCmd))]
public class PlaceTantrumPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<PlaceTantrumPatch>(85, instructions);
        
        LocalBuilder localBuilder = generator.DeclareLocal(typeof(PlayerPlaceTantrumEventArgs));

        Label retLabel = generator.DefineLabel();
        const int offset = -3;
        int index = newInstructions.FindIndex(i =>
            i.Calls(AccessTools.Method(typeof(NetworkServer), nameof(NetworkServer.Spawn), new[] { typeof(GameObject), typeof(NetworkConnection) }))) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPlaceTantrumEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, localBuilder.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp173EventsHandler), nameof(Scp173EventsHandler.OnPlayerPlaceTantrum))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerPlaceTantrumEventArgs), nameof(PlayerPlaceTantrumEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}