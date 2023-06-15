// -----------------------------------------------------------------------
// <copyright file="UseLungePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp939;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.UsingLungeAbility))]
[HarmonyPatch(typeof(Scp939LungeAbility), nameof(Scp939LungeAbility.TriggerLunge))]
public class UseLungePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<UseLungePatch>(8, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp939UsingLungeAbilityEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnUsingLungeAbility))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}