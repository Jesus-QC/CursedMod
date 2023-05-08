// -----------------------------------------------------------------------
// <copyright file="RemoveSavedRecordingPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp939;
using CursedMod.Events.Handlers.SCPs.Scp939;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(Scp939EventsHandler), nameof(Scp939EventsHandler.PlayerRemoveSavedVoice))]
[HarmonyPatch(typeof(MimicryRecorder), nameof(MimicryRecorder.RemoveRecordingsOfPlayer))]
public class RemoveSavedRecordingPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RemoveSavedRecordingPatch>(38, instructions);
        
        Label returnLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerRemoveSavedVoiceEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp939EventsHandler), nameof(Scp939EventsHandler.OnPlayerRemoveSavedVoice))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerRemoveSavedVoiceEventArgs), nameof(PlayerRemoveSavedVoiceEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}