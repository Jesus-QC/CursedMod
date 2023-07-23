// -----------------------------------------------------------------------
// <copyright file="SaveVoicePatch.cs" company="CursedMod">
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

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.SavingVoice))]
[HarmonyPatch(typeof(MimicryRecorder), nameof(MimicryRecorder.SaveRecording))]
public class SaveVoicePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<SaveVoicePatch>(37, instructions);

        Label retLabel = generator.DefineLabel();
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp939SavingVoiceEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnSavingVoice))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp939SavingVoiceEventArgs), nameof(Scp939SavingVoiceEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp939SavedVoiceEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnSavedVoice))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}