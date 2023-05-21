// -----------------------------------------------------------------------
// <copyright file="PlayerSaveVoiceEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class PlayerSaveVoiceEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerSaveVoiceEventArgs(MimicryRecorder recorder, ReferenceHub target)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(recorder.Owner);
        Target = CursedPlayer.Get(target);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedPlayer Target { get; }
}