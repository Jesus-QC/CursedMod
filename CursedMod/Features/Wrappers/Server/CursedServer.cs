using System;
using CursedMod.Features.Wrappers.Player.Dummies;
using Mirror.LiteNetLib4Mirror;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Server;

public static class CursedServer
{
    private static CursedDummy _local;
    
    public static CursedDummy LocalPlayer => _local ??= new CursedDummy(ReferenceHub.HostHub);

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

    public static bool IdleMode
    {
        get => global::IdleMode.IdleModeActive;
        set => global::IdleMode.SetIdleMode(value);
    }

    public static bool IdleModePaused
    {
        get => global::IdleMode.PauseIdleMode;
        set => global::IdleMode.PauseIdleMode = value;
    }

    public static bool IdleModeActive
    {
        get => global::IdleMode.IdleModeActive;
        set => global::IdleMode.IdleModeActive = value;
    }

    public static short IdleModeTickRate
    {
        get => global::IdleMode.IdleModeTickrate;
        set => global::IdleMode.IdleModeTickrate = value;
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
    
    public static bool AdminConnected => PlayerList._anyAdminOnServer;
    
    public static bool IsVerified => ServerStatic.PermissionsHandler.IsVerified;
    
    public static string IpAddress => ServerConsole.Ip;
    
    public static double Ticks => Math.Round(1f / Time.smoothDeltaTime);
    
    public static double Frames => Math.Round(1f / Time.deltaTime);

    public static bool IsBeta => GameCore.Version.PublicBeta || GameCore.Version.PrivateBeta;
    
    public static bool IsDedicated => ServerStatic.IsDedicated;

    public static void RefreshServerName() => ServerConsole.singleton.RefreshServerName();

    public static void RefreshServerData() => ServerConsole.singleton.RefreshServerData();
    
    public static void SendCommand(string command, CommandSender sender = null) => GameCore.Console.singleton.TypeCommand(command, sender ?? LocalPlayer.Sender);
}