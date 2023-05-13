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
using CursedMod.Features.Wrappers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Patches.SCPs.Scp939;

[DynamicEventPatch(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.PlayerSaveVoice))]
[HarmonyPatch(typeof(MimicryRecorder), nameof(MimicryRecorder.OnAnyPlayerKilled))]
public class SaveVoicePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<SaveVoicePatch>(52, instructions);

        Label retLabel = generator.DefineLabel();

        const int offset = 3;
        int index = newInstructions.FindIndex(i =>
            i.Calls(AccessTools.Method(typeof(MimicryRecorder), nameof(MimicryRecorder.IsPrivacyAccepted)))) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayer), nameof(CursedPlayer.Get), new[] { typeof(ReferenceHub) })),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerSaveVoiceEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp939EventsHandler), nameof(CursedScp939EventsHandler.OnPlayerSaveVoice))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerSaveVoiceEventArgs), nameof(PlayerSaveVoiceEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}