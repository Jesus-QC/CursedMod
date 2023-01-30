// -----------------------------------------------------------------------
// <copyright file="CursedServer.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CursedMod.Features.Wrappers.Player;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Server;

public static class CursedServer
{
    public static CursedPlayer LocalPlayer { get; internal set; }

    public static ushort Port
    {
        get => ServerStatic.ServerPort;
        set => ServerStatic.ServerPort = value;
    }

    public static bool ForwardPorts
    {
        get => LiteNetLib4MirrorTransport.Singleton.useUpnP;
        set => LiteNetLib4MirrorTransport.Singleton.useUpnP = value;
    }

    public static int MaxPlayerSlots
    {
        get => CustomNetworkManager.slots;
        set => CustomNetworkManager.slots = value;
    }

    public static int MaxReservedSlots
    {
        get => CustomNetworkManager.reservedSlots;
        set => CustomNetworkManager.reservedSlots = value;
    }

    public static bool Modded
    {
        get => CustomNetworkManager.Modded;
        set => CustomNetworkManager.Modded = value;
    }

    public static bool HeavilyModded
    {
        get => CustomNetworkManager.HeavilyModded;
        set => CustomNetworkManager.HeavilyModded = value;
    }

    public static bool IsFriendlyFireEnabled
    {
        get => ServerConsole.FriendlyFire;
        set => ServerConsole.FriendlyFire = value;
    }

    public static bool IsWhitelistEnabled
    {
        get => ServerConsole.WhiteListEnabled;
        set => ServerConsole.WhiteListEnabled = value;
    }
    
    public static ServerStatic.NextRoundAction NextRoundAction
    {
        get => ServerStatic.StopNextRound;
        set => ServerStatic.StopNextRound = value;
    }

    public static bool IsInIdleMode
    {
        get => IdleMode.IdleModeActive;
        set => IdleMode.SetIdleMode(value);
    }

    public static bool IdleModePaused
    {
        get => IdleMode.PauseIdleMode;
        set => IdleMode.PauseIdleMode = value;
    }

    public static bool IdleModeActive
    {
        get => IdleMode.IdleModeActive;
        set => IdleMode.IdleModeActive = value;
    }

    public static short IdleModeTickRate
    {
        get => IdleMode.IdleModeTickrate;
        set => IdleMode.IdleModeTickrate = value;
    }

    public static string PlayerListTitle
    {
        get => PlayerList.ServerName;
        set => PlayerList.ServerName = value;
    }

    public static float PlayerListTitleRefreshTime
    {
        get => PlayerList._refreshRate.Value;
        set => PlayerList._refreshRate.Value = value;
    }

    public static Dictionary<ItemCategory, sbyte> SyncedCategoryLimits
    {
        get
        {
            Dictionary<ItemCategory, sbyte> ret = new ();
            
            for (int i = 0; i < ServerConfigSynchronizer.Singleton.CategoryLimits.Count; i++)
            {
                ret.Add((ItemCategory)i, ServerConfigSynchronizer.Singleton.CategoryLimits[i]);
            }

            return ret;
        }
        
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                ServerConfigSynchronizer.Singleton.CategoryLimits[i] = value[(ItemCategory)i];
            }
        }
    }

    public static SyncList<ServerConfigSynchronizer.AmmoLimit> SyncedAmmoLimits =>
        ServerConfigSynchronizer.Singleton.AmmoLimitsSync;
    
    public static SyncList<ServerConfigSynchronizer.PredefinedBanTemplate> RemoteAdminPredefinedBanTemplates 
        => ServerConfigSynchronizer.Singleton.RemoteAdminPredefinedBanTemplates;

    public static bool EnableRemoteAdminPredefinedBanTemplates
    {
        get => ServerConfigSynchronizer.Singleton.EnableRemoteAdminPredefinedBanTemplates;
        set => ServerConfigSynchronizer.Singleton.NetworkEnableRemoteAdminPredefinedBanTemplates = value;
    }

    public static string RemoteAdminExternalLookupMode
    {
        get => ServerConfigSynchronizer.Singleton.RemoteAdminExternalPlayerLookupMode;
        set => ServerConfigSynchronizer.Singleton.NetworkRemoteAdminExternalPlayerLookupMode = value;
    }

    public static string RemoteAdminExternalPlayerLookupURL
    {
        get => ServerConfigSynchronizer.Singleton.RemoteAdminExternalPlayerLookupURL;
        set => ServerConfigSynchronizer.Singleton.NetworkRemoteAdminExternalPlayerLookupURL = value;
    }

    public static string ServerName
    {
        get => ServerConfigSynchronizer.Singleton.ServerName;
        set => ServerConfigSynchronizer.Singleton.NetworkServerName = value;
    }
    
    public static HashSet<string> ReservedSlotUsers => ReservedSlot.Users;

    public static bool AdminConnected => PlayerList._anyAdminOnServer;
    
    public static bool IsVerified => ServerStatic.PermissionsHandler.IsVerified;
    
    public static string IpAddress => ServerConsole.Ip;
    
    public static double Ticks => Math.Round(1f / Time.smoothDeltaTime);
    
    public static double Frames => Math.Round(1f / Time.deltaTime);

    public static bool IsBeta => GameCore.Version.PublicBeta || GameCore.Version.PrivateBeta;
    
    public static bool IsDedicated => ServerStatic.IsDedicated;
    
    public static string[] StartArguments => StartupArgs.Args;

    public static void RefreshServerName() => ServerConsole.singleton.RefreshServerName();

    public static void RefreshServerData() => ServerConsole.singleton.RefreshServerData();
    
    public static void SendCommand(string command, CommandSender sender = null) => GameCore.Console.singleton.TypeCommand(command, sender ?? LocalPlayer.Sender);
}