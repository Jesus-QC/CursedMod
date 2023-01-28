using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers.Round;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Round;

[HarmonyPatch(typeof(CharacterClassManager), nameof(CharacterClassManager.Init))]
public class WaitingForPlayersPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions,
        ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<WaitingForPlayersPatch>(6, instructions);

        Label skip = generator.DefineLabel();
        
        newInstructions[0].labels.Add(skip);
        
        newInstructions.InsertRange(0, new List<CodeInstruction>()
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(CharacterClassManager), nameof(CharacterClassManager.isLocalPlayer))),
            new (OpCodes.Brfalse_S, skip),
            new (OpCodes.Call, AccessTools.Method(typeof(RoundEventHandlers), nameof(RoundEventHandlers.OnWaitingForPlayers))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}