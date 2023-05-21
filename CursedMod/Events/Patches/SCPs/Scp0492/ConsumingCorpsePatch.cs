// -----------------------------------------------------------------------
// <copyright file="ConsumingCorpsePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp0492;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Scp049.Zombies;

namespace CursedMod.Events.Patches.SCPs.Scp0492;

[DynamicEventPatch(typeof(CursedScp0492EventsHandler), nameof(CursedScp0492EventsHandler.ConsumingCorpse))]
[HarmonyPatch(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.ServerProcessCmd))]
public class ConsumingCorpsePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ConsumingCorpsePatch>(94, instructions);
        
        Label retLabel = generator.DefineLabel();
        const int offset = -1;
        int index = newInstructions.FindLastIndex(i =>
            i.LoadsField(AccessTools.Field(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>._errorCode)))) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp0492ConsumingCorpseEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp0492EventsHandler), nameof(CursedScp0492EventsHandler.OnConsumingCorpse))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp0492ConsumingCorpseEventArgs), nameof(Scp0492ConsumingCorpseEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}