// -----------------------------------------------------------------------
// <copyright file="PlayerEnablingScp914EventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using Scp914;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class PlayerEnablingScp914EventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerEnablingScp914EventArgs(Scp914Controller controller, ReferenceHub player)
    {
        IsAllowed = true;
        Scp914Controller = controller;
        Player = CursedPlayer.Get(player);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public Scp914Controller Scp914Controller { get; }
}