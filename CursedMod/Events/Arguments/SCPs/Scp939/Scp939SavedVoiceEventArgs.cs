// -----------------------------------------------------------------------
// <copyright file="Scp939SavedVoiceEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939.Mimicry;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939SavedVoiceEventArgs : EventArgs, ICursedPlayerEvent
{
    public Scp939SavedVoiceEventArgs(MimicryRecorder recorder)
    {
        SavedVoice = recorder.SavedVoices[recorder.SavedVoices.Count - 1];
        Player = CursedPlayer.Get(SavedVoice.Owner.Hub);
    }
    
    public CursedPlayer Player { get; }
    
    public MimicryRecorder.MimicryRecording SavedVoice { get; }
}