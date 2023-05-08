// -----------------------------------------------------------------------
// <copyright file="OBanUserPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CommandSystem.Commands.RemoteAdmin;
using CursedMod.Events.Arguments.BanSystem;
using CursedMod.Events.Handlers.BanSystem;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.BanSystem;

[DynamicEventPatch(typeof(BanSystemEventsHandler), nameof(BanSystemEventsHandler.BanningOfflinePlayer))]
[HarmonyPatch(typeof(OfflineBanCommand), nameof(OfflineBanCommand.Execute))]
public class OBanUserPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<OBanUserPatch>(258, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(BanningOfflinePlayerEventArgs));
        
        int offset = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Pop) - 2;
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_2).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(BanningOfflinePlayerEventArgs))[0]),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(BanSystemEventsHandler), nameof(BanSystemEventsHandler.OnBanningOfflinePlayer))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(BanningOfflinePlayerEventArgs), nameof(BanningOfflinePlayerEventArgs.BanDetails))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(BanningOfflinePlayerEventArgs), nameof(BanningOfflinePlayerEventArgs.BanType))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}