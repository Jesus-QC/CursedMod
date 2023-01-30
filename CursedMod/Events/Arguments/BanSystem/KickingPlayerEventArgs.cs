// -----------------------------------------------------------------------
// <copyright file="KickingPlayerEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.BanSystem;

public class KickingPlayerEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public KickingPlayerEventArgs(ReferenceHub target, ICommandSender issuer, string reason)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(target);
        Issuer = CursedPlayer.Get(issuer);
        Reason = reason;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public CursedPlayer Issuer { get; }
    
    public string Reason { get; set; }
}