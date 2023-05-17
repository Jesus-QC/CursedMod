// -----------------------------------------------------------------------
// <copyright file="PlayerChangeKnobSettingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using Scp914;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class PlayerChangeKnobSettingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerChangeKnobSettingEventArgs(CursedPlayer player, Scp914KnobSetting knobSetting)
    {
        IsAllowed = true;
        Player = player;
        KnobSetting = knobSetting;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public Scp914KnobSetting KnobSetting { get; set; }
}