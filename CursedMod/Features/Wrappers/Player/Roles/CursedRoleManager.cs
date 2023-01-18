using CursedMod.Features.Extensions;
using PlayerRoles;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Roles;

public static class CursedRoleManager
{
    public static Vector3 GetRoleSpawnPosition(RoleTypeId role) => role.GetRandomSpawnPosition();
}