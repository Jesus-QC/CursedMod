// -----------------------------------------------------------------------
// <copyright file="PlayMimicrySoundPatch.cs" company="CursedMod">
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
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlayingSound))]
[HarmonyPatch(typeof(EnvironmentalMimicry), nameof(EnvironmentalMimicry.ServerProcessCmd))]
public class PlayMimicrySoundPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<PlayMimicrySoundPatch>(21, instructions);
        
        Label returnLabel = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(Scp939PlayingSoundEventArgs));
        
        int index = newInstructions.FindIndex(x => x.StoresField(AccessTools.Field(typeof(EnvironmentalMimicry), nameof(EnvironmentalMimicry._syncOption))));
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp939PlayingSoundEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnPlayingSound))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp939PlayingSoundEventArgs), nameof(Scp939PlayingSoundEventArgs.IsAllowed))),
            new (OpCodes.Brfalse, returnLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp939PlayingSoundEventArgs), nameof(Scp939PlayingSoundEventArgs.SelectedOption))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}