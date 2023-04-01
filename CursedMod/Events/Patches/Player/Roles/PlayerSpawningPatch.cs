// -----------------------------------------------------------------------
// <copyright file="PlayerSpawningPatch.cs" company="CursedMod">
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
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles.FirstPersonControl;
using RelativePositioning;

namespace CursedMod.Events.Patches.Player.Roles;

// [HarmonyPatch(typeof(FpcStandardRoleBase), nameof(FpcStandardRoleBase.ReadSpawnData))]
public class SpawningPlayerPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<SpawningPlayerPatch>(35, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(PlayerSpawningEventArgs));

        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        newInstructions.RemoveRange(0, 10);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(RelativePositionSerialization), nameof(RelativePositionSerialization.ReadRelativePosition))),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(NetworkReaderExtensions), nameof(NetworkReaderExtensions.ReadUInt16))),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerSpawningEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerEventsHandler), nameof(PlayerEventsHandler.OnPlayerSpawning))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSpawningEventArgs), nameof(PlayerSpawningEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSpawningEventArgs), nameof(PlayerSpawningEventArgs.SpawnPosition))),
            new (OpCodes.Stloc_0),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.PropertyGetter(typeof(FpcStandardRoleBase), nameof(FpcStandardRoleBase.FpcModule))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(FirstPersonMovementModule), nameof(FirstPersonMovementModule.MouseLook))),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSpawningEventArgs), nameof(PlayerSpawningEventArgs.SpawnRotation))),
            new (OpCodes.Ldc_I4, 0x7fff),
            new (OpCodes.Callvirt, AccessTools.Method(typeof(FpcMouseLook), nameof(FpcMouseLook.ApplySyncValues))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}