// -----------------------------------------------------------------------
// <copyright file="ServerThrowPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Items;
using CursedMod.Events.Handlers.Items;
using HarmonyLib;
using InventorySystem.Items.ThrowableProjectiles;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items.ThrowableProjectiles;

[HarmonyPatch(typeof(ThrowableItem), nameof(ThrowableItem.ServerThrow))]
public class ServerThrowPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerThrowPatch>(82, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(PlayerThrowingItemEventArgs));
        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Ldarg_S, 4),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerThrowingItemEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.OnPlayerThrowingItem))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerThrowingItemEventArgs), nameof(PlayerThrowingItemEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerThrowingItemEventArgs), nameof(PlayerThrowingItemEventArgs.ForceAmount))),
            new (OpCodes.Starg_S, 1),
            
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerThrowingItemEventArgs), nameof(PlayerThrowingItemEventArgs.UpwardFactor))),
            new (OpCodes.Starg_S, 2),
            
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerThrowingItemEventArgs), nameof(PlayerThrowingItemEventArgs.Torque))),
            new (OpCodes.Starg_S, 3),
            
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerThrowingItemEventArgs), nameof(PlayerThrowingItemEventArgs.StartVelocity))),
            new (OpCodes.Starg_S, 4),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}