// -----------------------------------------------------------------------
// <copyright file="RaProcessQueryPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using CommandSystem;
using CursedMod.Events.Arguments.CommandSystem;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using RemoteAdmin;

namespace CursedMod.Events.Patches.CommandSystem;

[DynamicEventPatch(typeof(CursedCommandSystemEventsHandler), nameof(CursedCommandSystemEventsHandler.ExecutingRemoteAdminCommand))]
[HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.ProcessQuery))]
public class RaProcessQueryPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RaProcessQueryPatch>(312, instructions);

        Label ret = generator.DefineLabel();
        LocalBuilder args = generator.DeclareLocal(typeof(ExecutingRemoteAdminCommandEventArgs));

        newInstructions[newInstructions.Count - 1].labels.Add(ret);
        
        int offset = newInstructions.FindIndex(x => x.opcode == OpCodes.Callvirt && x.operand is MethodInfo info && info == AccessTools.Method(typeof(CommandHandler), nameof(CommandHandler.TryGetCommand))) + 2;
        
        newInstructions.InsertRange(offset, new CodeInstruction[]
        {
            // args = new ExecutingCommandEventArgs(command, args, sender);
            new (OpCodes.Ldloc_2),
            new (OpCodes.Ldloc_1),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(ExecutingRemoteAdminCommandEventArgs))[0]),
            new (OpCodes.Stloc_S, args.LocalIndex),
            
            // CommandSystemEventsHandler.OnExecutingCommand(args);
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedCommandSystemEventsHandler), nameof(CursedCommandSystemEventsHandler.OnExecutingRemoteAdminCommand))),
            
            // if (!args.IsAllowed) return;
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ExecutingRemoteAdminCommandEventArgs), nameof(ExecutingRemoteAdminCommandEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, ret),
            
            // command = args.Command;
            // arguments = args.FullArguments;
            // sender = args.Sender;
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ExecutingRemoteAdminCommandEventArgs), nameof(ExecutingRemoteAdminCommandEventArgs.Command))),
            new (OpCodes.Stloc_2),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ExecutingRemoteAdminCommandEventArgs), nameof(ExecutingRemoteAdminCommandEventArgs.FullArguments))),
            new (OpCodes.Stloc_1),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ExecutingRemoteAdminCommandEventArgs), nameof(ExecutingRemoteAdminCommandEventArgs.Sender))),
            new (OpCodes.Starg_S, 1),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}