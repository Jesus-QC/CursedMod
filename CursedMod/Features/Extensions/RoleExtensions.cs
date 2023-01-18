using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using UnityEngine;

namespace CursedMod.Features.Extensions;

public static class RoleExtensions
{
    public static Vector3 GetRandomSpawnPosition(this RoleTypeId roleType)
    {
        if(!PlayerRoleLoader.TryGetRoleTemplate(roleType, out PlayerRoleBase roleBase))
            return Vector3.zero;
            
        if(roleBase is not IFpcRole fpc)
            return Vector3.zero;

        ISpawnpointHandler spawn = fpc.SpawnpointHandler;
        if(spawn is null)
            return Vector3.zero;

        if (spawn.TryGetSpawnpoint(out Vector3 pos, out float _))
            return pos;

        return Vector3.zero;
    }
}