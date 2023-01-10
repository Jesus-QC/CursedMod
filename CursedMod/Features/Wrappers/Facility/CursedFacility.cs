using CursedMod.Features.Wrappers.Server;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedFacility
{
    private static Broadcast _broadcast;
    private static AmbientSoundPlayer _ambientSoundPlayer;

    public static Broadcast Broadcast => _broadcast ??= Broadcast.Singleton;

    public static AmbientSoundPlayer AmbientSoundPlayer => _ambientSoundPlayer ??= CursedServer.LocalPlayer.GetComponent<AmbientSoundPlayer>();

    public static void PlayAmbientSound(int id) => AmbientSoundPlayer.RpcPlaySound(id);
}