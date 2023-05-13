// -----------------------------------------------------------------------
// <copyright file="ServerInteractScp330Patch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp330;
using CursedMod.Events.Handlers;
using HarmonyLib;
using Interactables.Interobjects;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.SCPs.Scp330;

[DynamicEventPatch(typeof(CursedScp330EventsHandler), nameof(CursedScp330EventsHandler.PlayerInteractingScp330))]
[HarmonyPatch(typeof(Scp330Interobject), nameof(Scp330Interobject.ServerInteract))]
public class ServerInteractScp330Patch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerInteractScp330Patch>(92, instructions);

        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Newarr) - 2;

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerInteractingScp330EventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp330EventsHandler), nameof(CursedScp330EventsHandler.OnPlayerInteractingScp330))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerInteractingScp330EventArgs), nameof(PlayerInteractingScp330EventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}