using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Arguments.Player;
using CursedMod.Events.Handlers.Player;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace CursedMod.Events.Patches.Player;

[HarmonyPatch(typeof(ReferenceHub), nameof(ReferenceHub.Start))]
public class ReferenceHubStartPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<ReferenceHubStartPatch>(19, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerJoinedEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerEventHandlers), nameof(PlayerEventHandlers.OnPlayerJoined))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}