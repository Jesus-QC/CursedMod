// -----------------------------------------------------------------------
// <copyright file="CursedCassie.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerStatsSystem;
using Respawning;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedCassie
{
    public static NineTailedFoxAnnouncer NineTailedFoxAnnouncer => NineTailedFoxAnnouncer.singleton;
    
    public static NineTailedFoxAnnouncer.VoiceLine[] VoiceLines 
        => NineTailedFoxAnnouncer.voiceLines;
    
    public static void AnnounceScpTermination(ReferenceHub scp, DamageHandlerBase hit) 
        => NineTailedFoxAnnouncer.AnnounceScpTermination(scp, hit);

    public static float CalculateDuration(string message, bool rawNumber = false, float speed = 1f)
        => NineTailedFoxAnnouncer.CalculateDuration(message, rawNumber, speed);

    public static void PlayAnnouncement(string message, bool makeHold = false, bool makeNoise = true, bool customAnnouncement = false) 
        => RespawnEffectsController.PlayCassieAnnouncement(message, makeHold, makeNoise, customAnnouncement);

    public static void PlayGlitchyPhrase(string phrase, float glitchChance = 0.5f, float jamChance = 0.5f) =>
        NineTailedFoxAnnouncer.ServerOnlyAddGlitchyPhrase(phrase, glitchChance, jamChance);

    public static void Clear() => RespawnEffectsController.ClearQueue();
}