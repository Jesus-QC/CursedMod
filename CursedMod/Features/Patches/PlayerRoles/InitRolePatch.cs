// -----------------------------------------------------------------------
// <copyright file="InitRolePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Player.Roles;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles;

namespace CursedMod.Features.Patches.PlayerRoles;

[HarmonyPatch(typeof(PlayerRoleBase), nameof(PlayerRoleBase.Init))]
public class InitRolePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<InitRolePatch>(13, instructions);

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(InitRolePatch), nameof(HandleRoleChange))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void HandleRoleChange(PlayerRoleBase roleBase, ReferenceHub hub)
    {
        if (!CursedPlayer.TryGet(hub, out CursedPlayer player))
            return;
        
        player.CurrentRole = CursedRole.Get(roleBase);
    }
}