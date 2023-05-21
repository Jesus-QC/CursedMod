// -----------------------------------------------------------------------
// <copyright file="PlayerChangingScp939KnobSettingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using Scp914;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class PlayerChangingScp939KnobSettingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerChangingScp939KnobSettingEventArgs(Scp914Controller controller, ReferenceHub player, Scp914KnobSetting knobSetting)
    {
        IsAllowed = true;
        Scp914Controller = controller;
        Player = CursedPlayer.Get(player);
        KnobSetting = knobSetting;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public Scp914KnobSetting KnobSetting { get; }
    
    public Scp914Controller Scp914Controller { get; }
}