using CursedMod.Features.Wrappers.Player.Ragdolls;

namespace CursedMod.Events.Internal;

public static class Ragdoll
{
    public static void OnSpawnedRagdoll(BasicRagdoll basicRagdoll) => CursedRagdoll.Ragdolls.Add(new CursedRagdoll(basicRagdoll));

    public static void OnRagdollRemoved(BasicRagdoll basicRagdoll) => CursedRagdoll.Ragdolls.Remove(CursedRagdoll.Get(basicRagdoll));
}