using VoiceChat.Playbacks;

namespace CursedMod.Features.Wrappers.Player.VoiceChat;

public class CursedVoicePlayback
{
    public VoiceChatPlaybackBase VoiceChatPlayback { get; }
    
    public CursedVoicePlayback(VoiceChatPlaybackBase voiceChatPlayback)
    {
        VoiceChatPlayback = voiceChatPlayback;
    }
    
    public float Loudness
    {
        get => VoiceChatPlayback.Loudness;
        set => VoiceChatPlayback.Loudness = value;
    }

    public float VolumeScale
    {
        get => VoiceChatPlayback.VolumeScale;
        set => VoiceChatPlayback.VolumeScale = value;
    }
}