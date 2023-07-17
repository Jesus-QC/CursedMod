// -----------------------------------------------------------------------
// <copyright file="FirearmRequestPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Items;
using CursedMod.Events.Handlers;
using HarmonyLib;
using InventorySystem.Items.Firearms.BasicMessages;
using NorthwoodLib.Pools;
using PluginAPI.Events;

namespace CursedMod.Events.Patches.Items;

[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerUnloadingWeapon))]
[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerReloadingWeapon))]
[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerTogglingAim))]
[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerDryfiringWeapon))]
[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerTogglinhWeaponFlashlight))]
[DynamicEventPatch(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.PlayerInspectingWeapon))]
[HarmonyPatch(typeof(FirearmBasicMessagesHandler), nameof(FirearmBasicMessagesHandler.ServerRequestReceived))]
public class FirearmRequestPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<FirearmRequestPatch>(211, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder toggleWeaponFlashlightArgs = generator.DeclareLocal(typeof(PlayerTogglingWeaponFlashlightEventArgs));
        
        int index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerUnloadWeaponEvent))[0])) - 2;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUnloadingWeaponEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerUnloadingWeapon))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUnloadingWeaponEventArgs), nameof(PlayerUnloadingWeaponEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerReloadWeaponEvent))[0])) - 2;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerReloadingWeaponEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerReloadingWeapon))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerReloadingWeaponEventArgs), nameof(PlayerReloadingWeaponEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerAimWeaponEvent))[0])) - 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Ldc_I4_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerTogglingAimEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerTogglingAim))),
        });

        index = newInstructions.FindLastIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerAimWeaponEvent))[0])) - 3;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Ldc_I4_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerTogglingAimEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerTogglingAim))),
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj &&
            i.OperandIs(AccessTools.GetDeclaredConstructors(typeof(PlayerDryfireWeaponEvent))[0])) - 2;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerDryfiringWeaponEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerDryfiringWeapon))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerDryfiringWeaponEventArgs), nameof(PlayerDryfiringWeaponEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ceq) - 7;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Ldloc_S, 6),
            new (OpCodes.Ldc_I4_0),
            new (OpCodes.Ceq),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerTogglingWeaponFlashlightEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, toggleWeaponFlashlightArgs.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerTogglingWeaponFlashlight))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerTogglingWeaponFlashlightEventArgs), nameof(PlayerTogglingWeaponFlashlightEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
            new (OpCodes.Ldloc_S, toggleWeaponFlashlightArgs.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerTogglingWeaponFlashlightEventArgs), nameof(PlayerTogglingWeaponFlashlightEventArgs.IsEmittingLight))),
            new (OpCodes.Ldc_I4_0),
            new (OpCodes.Ceq),
            new (OpCodes.Stloc_S, 6),
        });

        index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldftn) - 3;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldloc_0),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerInspectingWeaponEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedItemsEventsHandler), nameof(CursedItemsEventsHandler.OnPlayerInspectingWeapon))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
