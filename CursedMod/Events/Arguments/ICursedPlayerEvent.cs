using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments;

public interface ICursedPlayerEvent
{
    public CursedPlayer Player { get; set; }
}