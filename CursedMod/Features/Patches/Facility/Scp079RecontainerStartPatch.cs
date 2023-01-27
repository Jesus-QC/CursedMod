using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events;
using CursedMod.Events.Patches.Achievements;
using CursedMod.Features.Wrappers.Facility.Props;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Features.Patches.Facility;

[HarmonyPatch(typeof(Scp079Recontainer), nameof(Scp079Recontainer.Start))]
public class Scp079RecontainerStartPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ServerAchievePatch>(9, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.PropertySetter(typeof(Cursed079Recontainer), nameof(Cursed079Recontainer.RecontainerBase)))
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}