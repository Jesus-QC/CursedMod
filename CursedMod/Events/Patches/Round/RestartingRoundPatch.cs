using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers.Round;
using HarmonyLib;
using NorthwoodLib.Pools;
using RoundRestarting;

namespace CursedMod.Events.Patches.Round;

[HarmonyPatch(typeof(RoundRestart), nameof(RoundRestart.InitiateRoundRestart))]
public class RestartingRoundPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions,
        ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RestartingRoundPatch>(91, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Call, AccessTools.Method(typeof(RoundEventHandlers), nameof(RoundEventHandlers.OnRestartingRound)))
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}