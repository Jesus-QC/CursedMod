// -----------------------------------------------------------------------
// <copyright file="CursedRespawnManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using PluginAPI.Core;
using Respawning;

namespace CursedMod.Features.Wrappers.Facility;

// unfinished
public static class CursedRespawnManager
{
    public static float TimeForNextSequence => RespawnManager.Singleton._timeForNextSequence;

    public static double ElapsedSequenceTime => RespawnManager.Singleton._stopwatch.Elapsed.TotalSeconds;

    public static double NtfChance => Respawn.NtfTickets * 100;

    public static double ChaosChance => Respawn.ChaosTickets * 100;
    
    public static int TimeTillRespawn => RespawnManager.Singleton.TimeTillRespawn;

    public static TimeSpan RemainingSequenceTime => TimeSpan.FromSeconds(TimeTillRespawn);

    public static RespawnManager.RespawnSequencePhase CurrentSequence => RespawnManager.Singleton._curSequence;

    public static SpawnableTeamType NextKnownTeam => Respawn.NextKnownTeam;

    public static void Spawn(SpawnableTeamType team, bool playEffects = false) => Respawn.Spawn(team, playEffects);

    public static void AddTickets(SpawnableTeamType team, float amount) => Respawn.AddTickets(team, amount);
}