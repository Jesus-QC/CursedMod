// -----------------------------------------------------------------------
// <copyright file="BanningOfflinePlayerEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.BanSystem;

public class BanningOfflinePlayerEventArgs : EventArgs
{
    public BanningOfflinePlayerEventArgs(BanDetails banDetails, BanHandler.BanType banType, ICommandSender sender)
    {
        Issuer = CursedPlayer.Get(sender);
        BanDetails = banDetails;
        BanType = banType;
    }
    
    public CursedPlayer Issuer { get; }
    
    public BanDetails BanDetails { get; set; }
    
    public BanHandler.BanType BanType { get; set; }
}