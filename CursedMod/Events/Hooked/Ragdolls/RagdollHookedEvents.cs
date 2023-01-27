using CursedMod.Features.Wrappers.Player.Ragdolls;

namespace CursedMod.Events.Hooked.Ragdolls;

public static class RagdollHookedEvents
{
    public static void OnSpawnedRagdoll(BasicRagdoll basicRagdoll) => CursedRagdoll.Ragdolls.Add(new CursedRagdoll(basicRagdoll));

    public static void OnRagdollRemoved(BasicRagdoll basicRagdoll) => CursedRagdoll.Ragdolls.Remove(CursedRagdoll.Get(basicRagdoll));
}