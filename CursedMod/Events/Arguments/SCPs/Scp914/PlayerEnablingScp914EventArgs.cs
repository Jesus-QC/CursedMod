// -----------------------------------------------------------------------
// <copyright file="PlayerEnablingScp914EventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Player;
using Scp914;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class PlayerEnablingScp914EventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerEnablingScp914EventArgs(ReferenceHub player)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(player);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public Scp914KnobSetting KnobSetting
    {
        get => CursedScp914.KnobSetting;
        set => CursedScp914.KnobSetting = value;
    }
}