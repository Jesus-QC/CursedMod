// -----------------------------------------------------------------------
// <copyright file="GainExperiencePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.SCPs.Scp079;
using CursedMod.Events.Handlers;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Patches.SCPs.Scp079;

[DynamicEventPatch(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.GainingExperience))]
[HarmonyPatch(typeof(Scp079TierManager), nameof(Scp079TierManager.ServerGrantExperience))]
public class GainExperiencePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = CursedEventManager.CheckEvent<GainExperiencePatch>(22, instructions);

        LocalBuilder args = generator.DeclareLocal(typeof(Scp079GainingExperienceEventArgs));
        Label returnLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ldarg_2),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(Scp079GainingExperienceEventArgs))[0]),
            new (OpCodes.Dup),
            new (OpCodes.Dup),
            new (OpCodes.Stloc_S, args.LocalIndex),
            new (OpCodes.Call, AccessTools.Method(typeof(CursedScp079EventsHandler), nameof(CursedScp079EventsHandler.OnGainingExperience))),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079GainingExperienceEventArgs), nameof(Scp079GainingExperienceEventArgs.IsAllowed))),
            new (OpCodes.Brfalse_S, returnLabel),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079GainingExperienceEventArgs), nameof(Scp079GainingExperienceEventArgs.Experience))),
            new (OpCodes.Starg_S, 1),
            new (OpCodes.Ldloc_S, args.LocalIndex),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Scp079GainingExperienceEventArgs), nameof(Scp079GainingExperienceEventArgs.HudTranslation))),
            new (OpCodes.Starg_S, 2),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}