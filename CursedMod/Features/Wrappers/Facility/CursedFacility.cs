using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedFacility
{
    public static Broadcast Broadcast => Broadcast.Singleton;
    public static AmbientSoundPlayer AmbientSoundPlayer { get; internal set; }

    public static void ShowBroadcast(string message, ushort duration = 5, Broadcast.BroadcastFlags flags = Broadcast.BroadcastFlags.Normal)
    {
        foreach (CursedPlayer player in CursedPlayer.Collection)
        {
            player.ShowBroadcast(message, duration, flags);
        }
    }

    public static void ShowHint(string message, int duration = 5)
    {
        foreach (CursedPlayer player in CursedPlayer.Collection)
        {
            player.ShowHint(message, duration);
        }
    }

    public static void ClearBroadcasts()
    {
        foreach (CursedPlayer player in CursedPlayer.Collection)
        {
            player.ClearBroadcasts();
        }
    }

    public static void PlayAmbientSound(int id) => AmbientSoundPlayer.RpcPlaySound(id);
}