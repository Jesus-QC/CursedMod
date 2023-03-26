// -----------------------------------------------------------------------
// <copyright file="CursedVoiceChat.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.Voice;
using VoiceChat;

namespace CursedMod.Features.Wrappers.Player.VoiceChat;

public class CursedVoiceChat
{
    internal CursedVoiceChat(VoiceModuleBase voiceModule)
    {
        VoiceModule = voiceModule;
    }
    
    public VoiceModuleBase VoiceModule { get; }

    public VoiceChatChannel LastChannel => VoiceModule._lastChannel;

    public VoiceChatChannel CurrentChannel
    {
        get => VoiceModule.CurrentChannel;
        set => VoiceModule.CurrentChannel = value;
    }

    public bool IsSpeaking => VoiceModule.IsSpeaking;

    public void CheckRateLimit() => VoiceModule.CheckRateLimit();

    public void ValidateReceive(ReferenceHub refHub, VoiceChatChannel channel) => VoiceModule.ValidateReceive(refHub, channel);
    
    public void ValidateSend(VoiceChatChannel channel) => VoiceModule.ValidateSend(channel);
}