// -----------------------------------------------------------------------
// <copyright file="ServerBlink.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp173;
using CursedMod.Events.Handlers.SCPs.Scp173;
using CursedMod.Features.Wrappers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Subroutines;

namespace CursedMod.Events.Patches.SCPs.Scp173;

[DynamicEventPatch(typeof(Scp173EventsHandler), nameof(Scp173EventsHandler.PlayerBlinking))]
[HarmonyPatch(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer.ServerBlink))]
public class ServerBlink
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerBlink>(21, instructions);
        
        LocalBuilder localBuilder = generator.DeclareLocal(typeof(PlayerBlinkingEventArgs));
        Label retLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.PropertyGetter(typeof(ScpStandardSubroutine<Scp173Role>), nameof(ScpStandardSubroutine<Scp173Role>.Owner))),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldfld, AccessTools.Field(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer._observers))),
            new (OpCodes.Ldfld, AccessTools.Field(typeof(Scp173ObserversTracker), nameof(Scp173ObserversTracker.Observers))),
            new (OpCodes.Call, AccessTools.Method(typeof(ServerBlink), nameof(GetObservers))),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerBlinkingEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc, localBuilder.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(Scp173EventsHandler), nameof(Scp173EventsHandler.OnPlayerBlinking))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PlayerBlinkingEventArgs), nameof(PlayerBlinkingEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
    
    private static List<CursedPlayer> GetObservers(IEnumerable<ReferenceHub> hubs) => hubs.Select(CursedPlayer.Get).ToList();
}