// -----------------------------------------------------------------------
// <copyright file="KickUserPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CommandSystem;
using CursedMod.Events.Arguments.BanSystem;
using CursedMod.Events.Handlers.BanSystem;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.BanSystem;

[HarmonyPatch(typeof(BanPlayer), nameof(BanPlayer.KickUser), typeof(ReferenceHub), typeof(ICommandSender), typeof(string))]
public class KickUserPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<KickUserPatch>(27, instructions);

        Label ret = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(KickingPlayerEventArgs));
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(KickingPlayerEventArgs))[0]),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(BanSystemEventsHandler), nameof(BanSystemEventsHandler.OnKickingPlayer))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(KickingPlayerEventArgs), nameof(KickingPlayerEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(KickingPlayerEventArgs), nameof(KickingPlayerEventArgs.Reason))),
            new (OpCodes.Starg_S, 2),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}