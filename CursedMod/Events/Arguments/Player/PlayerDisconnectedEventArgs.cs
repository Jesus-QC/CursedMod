using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.Player;

public class PlayerDisconnectedEventArgs : EventArgs, ICursedPlayerEvent
{
    public CursedPlayer Player { get; set; }

    public PlayerDisconnectedEventArgs(ReferenceHub hub)
    {
        Player = CursedPlayer.Get(hub);
    }
}