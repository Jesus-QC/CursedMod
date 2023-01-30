// -----------------------------------------------------------------------
// <copyright file="BanSystemEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.BanSystem;
using CursedMod.Features.Logger;

namespace CursedMod.Events.Handlers.BanSystem;

public static class BanSystemEventsHandler
{
    public static event EventManager.CursedEventHandler<BanningPlayerEventArgs> BanningPlayer;
    
    public static event EventManager.CursedEventHandler<KickingPlayerEventArgs> KickingPlayer;

    public static void OnBanningPlayer(BanningPlayerEventArgs args)
    {
        CursedLogger.InternalDebug($"Banning player, {args.Player.DisplayNickname} by {args.Issuer.DisplayNickname}, {args.Reason}, {args.Duration}");
        args.IsAllowed = false;
        BanningPlayer.InvokeEvent(args);
    }

    public static void OnKickingPlayer(KickingPlayerEventArgs args)
    {
        CursedLogger.InternalDebug($"Kicking player, {args.Player.DisplayNickname} by {args.Issuer.DisplayNickname}, {args.Reason}");
        args.IsAllowed = false;
        KickingPlayer.InvokeEvent(args);
    }
}