// -----------------------------------------------------------------------
// <copyright file="BanningPlayerEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using CursedMod.Features.Wrappers.Player;
using Footprinting;

namespace CursedMod.Events.Arguments.BanSystem;

public class BanningPlayerEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public BanningPlayerEventArgs(Footprint target, ICommandSender issuer, string reason, long duration)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(target.Hub);
        Issuer = CursedPlayer.Get(issuer);
        Reason = reason;
        Duration = duration;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public CursedPlayer Issuer { get; }
    
    public string Reason { get; set; }
    
    public long Duration { get; set; }
}