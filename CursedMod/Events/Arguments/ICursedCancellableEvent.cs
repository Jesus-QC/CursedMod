namespace CursedMod.Events.Arguments;

public interface ICursedCancellableEvent
{
    public bool IsAllowed { get; set; }
}