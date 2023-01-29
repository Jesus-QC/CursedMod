// -----------------------------------------------------------------------
// <copyright file="CursedFacility.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedFacility
{
    public static Broadcast Broadcast => Broadcast.Singleton;
    
    public static AmbientSoundPlayer AmbientSoundPlayer { get; internal set; }

    public static void ShowBroadcast(string message, ushort duration = 5, Broadcast.BroadcastFlags flags = Broadcast.BroadcastFlags.Normal)
    {
        foreach (CursedPlayer player in CursedPlayer.Collection)
        {
            player.ShowBroadcast(message, duration, flags);
        }
    }

    public static void ShowHint(string message, int duration = 5)
    {
        foreach (CursedPlayer player in CursedPlayer.Collection)
        {
            player.ShowHint(message, duration);
        }
    }

    public static void ClearBroadcasts()
    {
        foreach (CursedPlayer player in CursedPlayer.Collection)
        {
            player.ClearBroadcasts();
        }
    }

    public static void ShowRoundSummary(RoundSummary.SumInfo_ClassList listStart, RoundSummary.SumInfo_ClassList listFinish, RoundSummary.LeadingTeam leadingTeam, int eDS, int eSc, int scpKills, int roundCd, int seconds)
        => RoundSummary.singleton.RpcShowRoundSummary(listStart, listFinish, leadingTeam, eDS, eSc, scpKills, roundCd, seconds);
    
    public static void PlayAmbientSound(int id) => AmbientSoundPlayer.RpcPlaySound(id);
}