// -----------------------------------------------------------------------
// <copyright file="PlayerSpawningPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.FirstPersonControl.Spawnpoints;

namespace CursedMod.Events.Patches.Player.Roles;

[DynamicEventPatch(typeof(PlayerEventsHandler), nameof(PlayerEventsHandler.Spawning))]
[HarmonyPatch]
public class SpawningPlayerPatch
{
    private static MethodInfo TargetMethod() => AccessTools.Method(typeof(RoleSpawnpointManager).GetNestedTypes(AccessTools.all)[1], "<Init>b__2_0");
    
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<SpawningPlayerPatch>(38, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(PlayerSpawningEventArgs));
        Label ret = generator.DefineLabel();
        
        newInstructions[newInstructions.Count - 1].labels.Add(ret);

        int offset = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Ldarg_1);

        newInstructions.InsertRange(offset, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[offset]),
            new (OpCodes.Ldarg_3),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Ldloc_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerSpawningEventArgs))[0]),
            
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerEventsHandler), nameof(PlayerEventsHandler.OnPlayerSpawning))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSpawningEventArgs), nameof(PlayerSpawningEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSpawningEventArgs), nameof(PlayerSpawningEventArgs.SpawnPosition))),
            new (OpCodes.Stloc_1),
 
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSpawningEventArgs), nameof(PlayerSpawningEventArgs.SpawnRotation))),
            new (OpCodes.Stloc_2),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}