using CursedMod.Features.Wrappers.Player.Ragdolls;

namespace CursedMod.Events.Internal;

public static class Ragdoll
{
    public static void OnSpawnedRagdoll(BasicRagdoll basicRagdoll) => CursedRagdoll.BasicRagdolls.Add(new CursedRagdoll(basicRagdoll));

    public static void OnRagdollRemoved(BasicRagdoll basicRagdoll) => CursedRagdoll.BasicRagdolls.Remove(CursedRagdoll.Get(basicRagdoll));
}