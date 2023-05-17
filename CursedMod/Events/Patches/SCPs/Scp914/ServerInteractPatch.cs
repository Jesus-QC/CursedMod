// -----------------------------------------------------------------------
// <copyright file="ServerInteractPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp914;
using CursedMod.Events.Handlers;
using CursedMod.Features.Wrappers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;
using PluginAPI.Enums;
using Scp914;

namespace CursedMod.Events.Patches.SCPs.Scp914;

[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.PlayerChangeKnobSetting))]
[DynamicEventPatch(typeof(CursedScp914EventsHandler), nameof(CursedScp914EventsHandler.PlayerStart))]
[HarmonyPatch(typeof(Scp914Controller), nameof(Scp914Controller.ServerInteract))]
public class ServerInteractPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerInteractPatch>(89, instructions);
        
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Call, AccessTools.Method(typeof(ServerInteractPatch), nameof(ProcessInteractEvents))),
            new (OpCodes.Br, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ProcessInteractEvents(Scp914Controller controller, ReferenceHub ply, byte colliderId)
    {
        if (controller._remainingCooldown > 0.0)
            return;

        switch ((Scp914InteractCode)colliderId)
        {
            case Scp914InteractCode.ChangeMode:
                Scp914KnobSetting scp914KnobSetting = controller._knobSetting + 1;
                if (!Enum.IsDefined(typeof(Scp914KnobSetting), scp914KnobSetting))
                    scp914KnobSetting = Scp914KnobSetting.Rough;
                
                if (!PluginAPI.Events.EventManager.ExecuteEvent(ServerEventType.Scp914KnobChange, ply, scp914KnobSetting, controller._knobSetting))
                    break;

                PlayerChangeKnobSettingEventArgs args = new (CursedPlayer.Get(ply), scp914KnobSetting);
                CursedScp914EventsHandler.OnPlayerChangeKnobSetting(args);
                
                if (!args.IsAllowed)
                    return;
                
                controller._remainingCooldown = controller._knobChangeCooldown;
                controller.Network_knobSetting = args.KnobSetting;
                controller.RpcPlaySound(0);
                break;
            case Scp914InteractCode.Activate:
                if (!PluginAPI.Events.EventManager.ExecuteEvent(ServerEventType.Scp914Activate, ply, controller._knobSetting))
                    break;
                
                PlayerStart914EventArgs args2 = new (CursedPlayer.Get(ply), controller._knobSetting);
                CursedScp914EventsHandler.OnPlayerStart914(args2);
                
                if (!args2.IsAllowed)
                    return;
                
                controller._remainingCooldown = controller._totalSequenceTime;
                controller._isUpgrading = true;
                controller._itemsAlreadyUpgraded = false;
                controller.RpcPlaySound(1);
                break;
        }
    }
}