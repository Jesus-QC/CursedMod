// -----------------------------------------------------------------------
// <copyright file="PlayerUsingVoiceChatEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using Mirror;
using VoiceChat.Networking;

namespace CursedMod.Events.Arguments.Player;

public class PlayerUsingVoiceChatEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerUsingVoiceChatEventArgs(NetworkConnection connection, VoiceMessage message)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(connection.identity);
        VoiceMessage = message;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public VoiceMessage VoiceMessage { get; set; }
}