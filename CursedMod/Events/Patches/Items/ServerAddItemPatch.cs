// -----------------------------------------------------------------------
// <copyright file="ServerAddItemPatch.cs" company="CursedMod">
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
using InventorySystem;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.PlayerPickedUpItem))]
[HarmonyPatch(typeof(InventoryExtensions), nameof(InventoryExtensions.ServerAddItem))]
public class ServerAddItemPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerAddItemPatch>(77, instructions);

        newInstructions.InsertRange(newInstructions.Count - 2, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerPickedUpItemEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(ItemsEventsHandler), nameof(ItemsEventsHandler.OnPlayerPickedUpItem))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}