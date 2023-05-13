// -----------------------------------------------------------------------
// <copyright file="TransceiverReceiveMessagePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using VoiceChat.Networking;

namespace CursedMod.Events.Patches.Player.VoiceChat;

[DynamicEventPatch(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.UsingVoiceChat))]
[HarmonyPatch(typeof(VoiceTransceiver), nameof(VoiceTransceiver.ServerReceiveMessage))]
public class TransceiverReceiveMessagePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<TransceiverReceiveMessagePatch>(84, instructions);

        Label ret = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(PlayerUsingVoiceChatEventArgs));

        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Ldloc_0);
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerUsingVoiceChatEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedPlayerEventsHandler), nameof(CursedPlayerEventsHandler.OnPlayerUsingVoiceChat))),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUsingVoiceChatEventArgs), nameof(PlayerUsingVoiceChatEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerUsingVoiceChatEventArgs), nameof(PlayerUsingVoiceChatEventArgs.VoiceMessage))),
            new (OpCodes.Starg_S, 1),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}