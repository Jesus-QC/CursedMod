using InventorySystem;
using InventorySystem.Configs;
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
    
    public static bool TryGetDefaultInventory(this RoleTypeId role, out InventoryRoleInfo inventoryRoleInfo) 
        => StartingInventories.DefinedInventories.TryGetValue(role, out inventoryRoleInfo);
    
    public static void SetDefaultInventory(this RoleTypeId role, InventoryRoleInfo inventoryRoleInfo) 
        => StartingInventories.DefinedInventories.SetOrAddElement(role, inventoryRoleInfo);
}