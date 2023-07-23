// -----------------------------------------------------------------------
// <copyright file="PermissionsManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using CommandSystem;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Server;
using Serialization;

namespace CursedMod.Loader.Permissions;

public static class PermissionsManager
{
    public static Dictionary<string, PermissionsGroup> PermissionsByGroup { get; set; } = new ();

    public static void Load()
    {
        CursedLogger.InternalDebug("Loading permissions...");
        PermissionsByGroup.Clear();
        if (!CursedPaths.Permissions.Exists)
        {
            LoadDefaultPermissions();
            return;
        }

        try
        {
            PermissionsByGroup = YamlParser.Deserializer.Deserialize<Dictionary<string, PermissionsGroup>>(File.ReadAllText(CursedPaths.Permissions.FullName));
            
            foreach (PermissionsGroup permissionsGroup in PermissionsByGroup.Values)
            {
                foreach (string permission in permissionsGroup.Permissions)
                {
                    if (!permission.Contains("."))
                        continue;
                    
                    int index = permission.LastIndexOf(".", StringComparison.Ordinal);
                    string perm = permission.Substring(0, index);
                    
                    permissionsGroup.SpecialPermissions.Add(perm + ".*");
                }
            }
            
            AddMissingGroups();
        }
        catch (Exception e)
        {
            CursedLogger.LogError("Couldn't load permissions. Loading default permissions.");
            CursedLogger.LogError(e);
            LoadDefaultPermissions(false);
        }
    }

    public static void AddMissingGroups()
    {
        if (!PermissionsByGroup.ContainsKey("default"))
            PermissionsByGroup.Add("default", new PermissionsGroup());
        
        foreach (string group in CursedServer.PermissionsHandler._groups.Keys)
        {
            if (PermissionsByGroup.ContainsKey(group))
                continue;
                
            PermissionsByGroup.Add(group, new PermissionsGroup
            {
                InheritedGroups = new[] { "default" },
            });
        }
        
        File.WriteAllText(CursedPaths.Permissions.FullName, YamlParser.Serializer.Serialize(PermissionsByGroup));
    }
    
    public static void LoadDefaultPermissions(bool save = true)
    {
        CursedLogger.InternalDebug("Loading default permissions...");

        PermissionsByGroup.Add("default", new PermissionsGroup());
        
        foreach (string group in CursedServer.PermissionsHandler._groups.Keys)
        {
            PermissionsByGroup.Add(group, new PermissionsGroup
            {
                InheritedGroups = new[] { "default" },
            });
        }

        if (!save)
            return;
        
        File.WriteAllText(CursedPaths.Permissions.FullName, YamlParser.Serializer.Serialize(PermissionsByGroup));
    }
    
    public static bool HasPermission(this PermissionsGroup permissionsGroup, string permission)
    {
        if (permissionsGroup.Permissions.Contains(permission) || permissionsGroup.SpecialPermissions.Contains(permission))
            return true;

        foreach (string inheritedGroup in permissionsGroup.InheritedGroups)
        {
            if (!PermissionsByGroup.TryGetValue(inheritedGroup, out PermissionsGroup inheritedPermissionsGroup))
                continue;

            if (inheritedPermissionsGroup.HasPermission(permission))
                return true;
        }

        return false;
    }

    public static bool HasPermission(this CursedPlayer player, string permission)
    {
        if (player == CursedServer.LocalPlayer)
            return true;
        
        if (player.Group is null)
        {
            return PermissionsByGroup.TryGetValue("default", out PermissionsGroup defaultGroup) && defaultGroup.HasPermission(permission);
        }
        
        return PermissionsByGroup.TryGetValue(player.GroupName, out PermissionsGroup permissionsGroup) && permissionsGroup.HasPermission(permission);
    }

    public static bool HasPermission(this ICommandSender commandSender, string permission) => CursedPlayer.Get(commandSender).HasPermission(permission);
}