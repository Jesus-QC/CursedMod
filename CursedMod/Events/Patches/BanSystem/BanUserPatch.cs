// -----------------------------------------------------------------------
// <copyright file="BanUserPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.BanSystem;
using CursedMod.Events.Handlers.BanSystem;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.BanSystem;

[HarmonyPatch(typeof(BanPlayer), nameof(BanPlayer.BanUser))]
public class BanUserPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<BanUserPatch>(118, instructions);

        Label ret = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(BanningPlayerEventArgs));

        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Box) + 4;
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(BanningPlayerEventArgs))[0]),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(BanSystemEventsHandler), nameof(BanSystemEventsHandler.OnBanningPlayer))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(BanningPlayerEventArgs), nameof(BanningPlayerEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(BanningPlayerEventArgs), nameof(BanningPlayerEventArgs.Reason))),
            new (OpCodes.Starg_S, 2),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(BanningPlayerEventArgs), nameof(BanningPlayerEventArgs.Duration))),
            new (OpCodes.Starg_S, 3),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}