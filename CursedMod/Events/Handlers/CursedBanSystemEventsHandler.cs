// -----------------------------------------------------------------------
// <copyright file="CursedBanSystemEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.BanSystem;

namespace CursedMod.Events.Handlers;

public static class CursedBanSystemEventsHandler
{
    public static event CursedEventManager.CursedEventHandler<BanningOfflinePlayerEventArgs> BanningOfflinePlayer;
    
    public static event CursedEventManager.CursedEventHandler<BanningPlayerEventArgs> BanningPlayer;
    
    public static event CursedEventManager.CursedEventHandler<KickingPlayerEventArgs> KickingPlayer;
    
    public static event CursedEventManager.CursedEventHandler<IssuingBanEventArgs> IssuingBan;

    public static event CursedEventManager.CursedEventHandler<LocalReportingEventArgs> LocalReporting;
    
    public static event CursedEventManager.CursedEventHandler<ReportingCheaterEventArgs> ReportingCheater; 

    internal static void OnBanningPlayer(BanningPlayerEventArgs args)
    {
        BanningPlayer.InvokeEvent(args);
    }

    internal static void OnKickingPlayer(KickingPlayerEventArgs args)
    {
        KickingPlayer.InvokeEvent(args);
    }

    internal static void OnBanningOfflinePlayer(BanningOfflinePlayerEventArgs args)
    {
        BanningOfflinePlayer.InvokeEvent(args);
    }
    
    internal static void OnIssuingBan(IssuingBanEventArgs args)
    {
        IssuingBan.InvokeEvent(args);
    }

    internal static void OnLocalReporting(LocalReportingEventArgs args)
    {
        LocalReporting.InvokeEvent(args);
    }
    
    internal static void OnReportingCheater(ReportingCheaterEventArgs args)
    {
        ReportingCheater.InvokeEvent(args);
    }
}
