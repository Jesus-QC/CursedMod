// -----------------------------------------------------------------------
// <copyright file="BanSystemEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.BanSystem;

namespace CursedMod.Events.Handlers.BanSystem;

public static class BanSystemEventsHandler
{
    public static event EventManager.CursedEventHandler<BanningOfflinePlayerEventArgs> BanningOfflinePlayer;
    
    public static event EventManager.CursedEventHandler<BanningPlayerEventArgs> BanningPlayer;
    
    public static event EventManager.CursedEventHandler<KickingPlayerEventArgs> KickingPlayer;
    
    public static event EventManager.CursedEventHandler<IssuingBanEventArgs> IssuingBan;

    public static void OnBanningPlayer(BanningPlayerEventArgs args)
    {
        BanningPlayer.InvokeEvent(args);
    }

    public static void OnKickingPlayer(KickingPlayerEventArgs args)
    {
        KickingPlayer.InvokeEvent(args);
    }

    public static void OnBanningOfflinePlayer(BanningOfflinePlayerEventArgs args)
    {
        BanningOfflinePlayer.InvokeEvent(args);
    }
    
    public static void OnIssuingBan(IssuingBanEventArgs args)
    {
        IssuingBan.InvokeEvent(args);
    }
    
}