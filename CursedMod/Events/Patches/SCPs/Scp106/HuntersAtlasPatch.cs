// -----------------------------------------------------------------------
// <copyright file="HuntersAtlasPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp106;
using CursedMod.Events.Handlers.SCPs.Scp106;
using CursedMod.Features.Logger;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Events.Patches.SCPs.Scp106;

[DynamicEventPatch(typeof(Scp106EventsHandler), nameof(Scp106EventsHandler.PlayerUseHunterAtlas))]
[HarmonyPatch(typeof(Scp106HuntersAtlasAbility), nameof(Scp106HuntersAtlasAbility.SetSubmerged), typeof(bool))]
public class HuntersAtlasPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<HuntersAtlasPatch>(25, instructions);
        
        Label returnLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUseHunterAtlasEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp106EventsHandler), nameof(Scp106EventsHandler.OnPlayerUseHunterAtlas))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUseHunterAtlasEventArgs), nameof(PlayerUseHunterAtlasEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (var instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Rent(newInstructions);
    }
}