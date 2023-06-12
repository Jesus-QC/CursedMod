// -----------------------------------------------------------------------
// <copyright file="ServerBlinkPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp173;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;

namespace CursedMod.Events.Patches.SCPs.Scp173;

[DynamicEventPatch(typeof(CursedScp173EventsHandler), nameof(CursedScp173EventsHandler.Blinking))]
[HarmonyPatch(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer.ServerBlink))]
public class ServerBlinkPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerBlinkPatch>(21, instructions);
        
        LocalBuilder args = generator.DeclareLocal(typeof(Scp173BlinkingEventArgs));
        Label retLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp173BlinkingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp173EventsHandler), nameof(CursedScp173EventsHandler.OnBlinking))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp173BlinkingEventArgs), nameof(Scp173BlinkingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp173BlinkingEventArgs), nameof(Scp173BlinkingEventArgs.TeleportPosition))),
            new (OpCodes.Starg_S, 1),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}