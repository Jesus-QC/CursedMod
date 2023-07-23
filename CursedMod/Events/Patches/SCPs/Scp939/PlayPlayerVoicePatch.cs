// -----------------------------------------------------------------------
// <copyright file="PlayPlayerVoicePatch.cs" company="CursedMod">
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
using Utils.Networking;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlayingVoice))]
[HarmonyPatch(typeof(MimicryRecorder), nameof(MimicryRecorder.ServerProcessCmd))]
public class PlayPlayerVoicePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<PlayPlayerVoicePatch>(29, instructions);
        
        LocalBuilder voiceOwner = generator.DeclareLocal(typeof(ReferenceHub));
        Label returnLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(x => x.Calls(AccessTools.Method(typeof(ReferenceHubReaderWriter), nameof(ReferenceHubReaderWriter.ReadReferenceHub)))) + 1;
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, voiceOwner.LocalIndex),
        });
        
        index = newInstructions.FindLastIndex(x => x.IsLdarg(0));
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldloc_S, voiceOwner.LocalIndex),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp939PlayingVoiceEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnPlayingVoice))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp939PlayingVoiceEventArgs), nameof(Scp939PlayingVoiceEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
} 