using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles;

namespace CursedMod.Events.Arguments.Player;

public class PlayerChangingRoleEventArgs : EventArgs, ICursedPlayerEvent, ICursedCancellableEvent
{
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public RoleTypeId NewRole { get; set; }
    
    public RoleChangeReason ChangeReason { get; set; }
    
    public RoleSpawnFlags SpawnFlags { get; set; }

    public PlayerChangingRoleEventArgs(PlayerRoleManager manager, RoleTypeId roleTypeId, RoleChangeReason reason,
        RoleSpawnFlags spawnFlags)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(manager._hub);
        NewRole = roleTypeId;
        ChangeReason = reason;
        SpawnFlags = spawnFlags;
    }
    
}