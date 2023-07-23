// -----------------------------------------------------------------------
// <copyright file="ReportingCheaterEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments.BanSystem;

public class ReportingCheaterEventArgs : EventArgs, ICursedPlayerEvent, ICursedCancellableEvent
{
    public ReportingCheaterEventArgs(ReferenceHub player, ReferenceHub reportedPlayer, string reason)
    {
        Player = CursedPlayer.Get(player);
        ReportedPlayer = CursedPlayer.Get(reportedPlayer);
        Reason = reason;
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }
    
    public CursedPlayer ReportedPlayer { get; }
    
    public string Reason { get; set; }
    
    public bool IsAllowed { get; set; } 
}