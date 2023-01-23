using PlayerRoles.Voice;
using VoiceChat;

namespace CursedMod.Features.Wrappers.Player.VoiceChat;

public class CursedVoiceChat
{
    public VoiceModuleBase VoiceModule { get; }

    public CursedVoiceChat(VoiceModuleBase voiceModule)
    {
        VoiceModule = voiceModule;
    }

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