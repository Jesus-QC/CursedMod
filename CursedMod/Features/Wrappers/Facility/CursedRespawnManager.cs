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

public static class CursedRespawnManager
{
    public static RespawnManager.RespawnSequencePhase CurrentSequence
    {
        get => RespawnManager.Singleton._curSequence;
        set => RespawnManager.Singleton._curSequence = value;
    }

    public static bool PrioritySpawn
    {
        get => RespawnManager.Singleton._prioritySpawn;
        set => RespawnManager.Singleton._prioritySpawn = value;
    }

    public static float TimeForNextSequence
    {
        get => RespawnManager.Singleton._timeForNextSequence;
        set => RespawnManager.Singleton._timeForNextSequence = value;
    }

    public static double ElapsedSequenceTime => RespawnManager.Singleton._stopwatch.Elapsed.TotalSeconds;

    public static float NtfTickets => Respawn.NtfTickets;

    public static float ChaosTickets => Respawn.ChaosTickets;
    
    public static float NtfChance => Respawn.NtfTickets * 100;

    public static float ChaosChance => Respawn.ChaosTickets * 100;
    
    public static int TimeTillRespawn => RespawnManager.Singleton.TimeTillRespawn;

    public static TimeSpan RemainingSequenceTime => TimeSpan.FromSeconds(TimeTillRespawn);

    public static SpawnableTeamType NextKnownTeam => Respawn.NextKnownTeam;
    
    public static SpawnableTeamType DominatingTeam => RespawnTokensManager.DominatingTeam;

    public static void Spawn(SpawnableTeamType team, bool playEffects = false) => Respawn.Spawn(team, playEffects);

    public static void AddTickets(SpawnableTeamType team, float amount) => Respawn.AddTickets(team, amount);

    public static void RemoveTickets(SpawnableTeamType team, float amount) => AddTickets(team, -amount);
    
    public static void ResetTickets() => RespawnTokensManager.ResetTokens();

    public static void ForceTeamDominance(SpawnableTeamType type, float dominance) => RespawnTokensManager.ForceTeamDominance(type, dominance);
}