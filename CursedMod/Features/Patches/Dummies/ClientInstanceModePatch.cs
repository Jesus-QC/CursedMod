// -----------------------------------------------------------------------
// <copyright file="ClientInstanceModePatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

/*using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Features.Wrappers.Player.Dummies;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Features.Patches.Dummies;

[HarmonyPatch(typeof(CharacterClassManager), nameof(CharacterClassManager.InstanceMode), MethodType.Setter)]
public class ClientInstanceModePatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

        Label skip = generator.DefineLabel();
        
        newInstructions[0].labels.Add(skip);
        
        newInstructions.InsertRange(0, new List<CodeInstruction>()
        {
            new (OpCodes.Ldsfld, AccessTools.Field(typeof(CursedDummy), nameof(CursedDummy.Dictionary))),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Ldfld, AccessTools.Field(typeof(CharacterClassManager), nameof(CharacterClassManager._hub))),
            new (OpCodes.Callvirt, AccessTools.Method(typeof(Dictionary<ReferenceHub, CursedDummy>), nameof(Dictionary<ReferenceHub, CursedDummy>.ContainsKey))),
            new (OpCodes.Brfalse_S, skip),
            new (OpCodes.Ldc_I4_2),
            new (OpCodes.Starg_S, 1),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}*/