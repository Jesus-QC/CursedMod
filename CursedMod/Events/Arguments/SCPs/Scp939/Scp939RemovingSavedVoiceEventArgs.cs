// -----------------------------------------------------------------------
// <copyright file="Scp939RemovingSavedVoiceEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939RemovingSavedVoiceEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp939RemovingSavedVoiceEventArgs(MimicryRecorder recorder, int savedVoice)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(recorder.Owner);
        SavedVoice = recorder.SavedVoices[savedVoice];
        VoiceOwner = CursedPlayer.Get(SavedVoice.Owner.Hub);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public CursedPlayer VoiceOwner { get; }
    
    public MimicryRecorder.MimicryRecording SavedVoice { get; }
}