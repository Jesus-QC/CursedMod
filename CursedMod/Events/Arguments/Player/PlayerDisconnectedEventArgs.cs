using System;
using CursedMod.Features.Wrappers.Player;
using Mirror;

namespace CursedMod.Events.Arguments.Player;

public class PlayerDisconnectedEventArgs : EventArgs, ICursedPlayerEvent
{
    public CursedPlayer Player { get; }

    public PlayerDisconnectedEventArgs(NetworkConnection connection)
    {
        Player = CursedPlayer.Get(connection.identity);
    }
}