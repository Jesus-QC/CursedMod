// -----------------------------------------------------------------------
// <copyright file="RoundSummaryPatch.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using CursedMod.Events.Handlers.Round;
using GameCore;
using HarmonyLib;
using MEC;
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Enums;
using PluginAPI.Events;
using RoundRestarting;
using UnityEngine;
using Utils.NonAllocLINQ;

namespace CursedMod.Events.Patches.Round;

[HarmonyPatch(typeof(RoundSummary), nameof(RoundSummary.Start))]
public class RoundSummaryPatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckEvent<RoundSummaryPatch>(41, instructions);

        newInstructions.Clear();
        
        newInstructions.AddRange(new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Call, AccessTools.Method(typeof(RoundSummaryPatch), nameof(StartRoundSummary))),
            new (OpCodes.Ret),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void StartRoundSummary(RoundSummary instance)
    {
        RoundSummary.singleton = instance;
        RoundSummary._singletonSet = true;
        
        if (!NetworkServer.active)
            return;
        
        RoundSummary.roundTime = 0;
        instance.KeepRoundOnOne = !ConfigFile.ServerConfig.GetBool("end_round_on_one_player");
        
        Timing.RunCoroutine(ProcessRoundCheck(instance), Segment.FixedUpdate);
        
        RoundSummary.KilledBySCPs = 0;
        RoundSummary.EscapedClassD = 0;
        RoundSummary.EscapedScientists = 0;
        RoundSummary.ChangedIntoZombies = 0;
        RoundSummary.Kills = 0;
        PlayerRoleManager.OnServerRoleSet += instance.OnRoleChanged;
        PlayerStats.OnAnyPlayerDied += instance.OnAnyPlayerDied;
    }
    
    private static IEnumerator<float> ProcessRoundCheck(RoundSummary instance)
    {
        float time = Time.unscaledTime; 
        while (instance != null) 
        { 
            yield return Timing.WaitForSeconds(2.5f);

            if (RoundSummary.RoundLock) 
                continue;
            
            if (instance.KeepRoundOnOne && ReferenceHub.AllHubs.Count(x => x.characterClassManager.InstanceMode != ClientInstanceMode.DedicatedServer) < 2)
                continue;

            if (!RoundSummary.RoundInProgress() || Time.unscaledTime - time < 15f)
                continue;
            
            RoundSummary.SumInfo_ClassList newList = default;
            
            foreach (ReferenceHub hub in ReferenceHub.AllHubs)
            {
                switch (hub.GetTeam())
                {
                    case Team.SCPs:
                        if (hub.GetRoleId() == RoleTypeId.Scp0492)
                            newList.zombies++;
                        else
                            newList.scps_except_zombies++;
                        break;
                    case Team.FoundationForces:
                        newList.mtf_and_guards++;
                        break;
                    case Team.ChaosInsurgency:
                        newList.chaos_insurgents++;
                        break;
                    case Team.Scientists:
                        newList.scientists++;
                        break;
                    case Team.ClassD:
                        newList.class_ds++;
                        break;
                    case Team.Dead:
                        break;
                    case Team.OtherAlive:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            yield return float.NegativeInfinity;
            
            newList.warhead_kills = AlphaWarheadController.Detonated ? AlphaWarheadController.Singleton.WarheadKills : -1;
            
            yield return float.NegativeInfinity;
            
            int facilityForces = newList.mtf_and_guards + newList.scientists;
            int chaosInsurgency = newList.chaos_insurgents + newList.class_ds;
            int anomalies = newList.scps_except_zombies + newList.zombies;
            int num = newList.class_ds + RoundSummary.EscapedClassD;
            int num2 = newList.scientists + RoundSummary.EscapedScientists;
            
            RoundSummary.SurvivingSCPs = newList.scps_except_zombies;
            
            float dEscapePercentage = instance.classlistStart.class_ds == 0 ? 0 : num / instance.classlistStart.class_ds;
            float sEscapePercentage = instance.classlistStart.scientists == 0 ? 1 : num2 / instance.classlistStart.scientists;
            
            bool flag;
            
            if (newList.class_ds <= 0 && facilityForces <= 0)
            {
                flag = true;
            }
            else
            {
                int num3 = 0;
                if (facilityForces > 0)
                    num3++;
                if (chaosInsurgency > 0)
                    num3++;
                if (anomalies > 0)
                    num3++;
                flag = num3 <= 1;
            }
            
            if (!instance._roundEnded)
            {
                RoundEndConditionsCheckCancellationData.RoundEndConditionsCheckCancellation cancellation = PluginAPI.Events.EventManager.ExecuteEvent<RoundEndConditionsCheckCancellationData>(ServerEventType.RoundEndConditionsCheck, flag).Cancellation;
                int num4 = (int)cancellation;
                if (num4 != 1)
                {
                    if (num4 == 2 && !instance._roundEnded)
                        continue;
                    
                    if (flag)
                        instance._roundEnded = true;
                }
                else
                {
                    instance._roundEnded = true;
                }
            }

            if (!instance._roundEnded) 
                continue;
            
            bool flag2 = facilityForces > 0;
            bool flag3 = chaosInsurgency > 0;
            bool flag4 = anomalies > 0;
            RoundSummary.LeadingTeam leadingTeam = RoundSummary.LeadingTeam.Draw;
            
            if (flag2)
            {
                leadingTeam = RoundSummary.EscapedScientists >= RoundSummary.EscapedClassD ? RoundSummary.LeadingTeam.FacilityForces : RoundSummary.LeadingTeam.Draw;
            }
            else if (flag4 || (flag4 && flag3))
            {
                leadingTeam = RoundSummary.EscapedClassD > RoundSummary.SurvivingSCPs ? RoundSummary.LeadingTeam.ChaosInsurgency : RoundSummary.SurvivingSCPs > RoundSummary.EscapedScientists ? RoundSummary.LeadingTeam.Anomalies : RoundSummary.LeadingTeam.Draw;
            }
            else if (flag3)
            {
                leadingTeam = RoundSummary.EscapedClassD >= RoundSummary.EscapedScientists ? RoundSummary.LeadingTeam.ChaosInsurgency : RoundSummary.LeadingTeam.Draw;
            }
            
            RoundEndCancellationData roundEndCancellationData = PluginAPI.Events.EventManager.ExecuteEvent<RoundEndCancellationData>(ServerEventType.RoundEnd, leadingTeam);
            
            while (roundEndCancellationData.IsCancelled)
            {
                if (roundEndCancellationData.Delay <= 0f)
                    yield break;

                yield return Timing.WaitForSeconds(roundEndCancellationData.Delay);
                roundEndCancellationData = PluginAPI.Events.EventManager.ExecuteEvent<RoundEndCancellationData>(ServerEventType.RoundEnd, leadingTeam);
            }
            
            RoundEventsHandler.OnRoundEnded();
            
            if (Statistics.FastestEndedRound.Duration > RoundStart.RoundLength)
                Statistics.FastestEndedRound = new Statistics.FastestRound(leadingTeam, RoundStart.RoundLength, DateTime.Now);

            Statistics.CurrentRound.ClassDAlive = newList.class_ds;
            Statistics.CurrentRound.ScientistsAlive = newList.scientists;
            Statistics.CurrentRound.MtfAndGuardsAlive = newList.mtf_and_guards;
            Statistics.CurrentRound.ChaosInsurgencyAlive = newList.chaos_insurgents;
            Statistics.CurrentRound.ZombiesAlive = newList.zombies;
            Statistics.CurrentRound.ScpsAlive = newList.scps_except_zombies;
            Statistics.CurrentRound.WarheadKills = newList.warhead_kills;
            FriendlyFireConfig.PauseDetector = true;
            
            string text = string.Concat("Round finished! Anomalies: ", anomalies, " | Chaos: ", chaosInsurgency, " | Facility Forces: ", facilityForces, " | D escaped percentage: ", dEscapePercentage, " | S escaped percentage: : ", sEscapePercentage);
            GameCore.Console.AddLog(text, Color.gray);
            
            ServerLogs.AddLog(ServerLogs.Modules.Logger, text, ServerLogs.ServerLogType.GameEvent);
            yield return Timing.WaitForSeconds(1.5f);
            int num5 = Mathf.Clamp(ConfigFile.ServerConfig.GetInt("auto_round_restart_time", 10), 5, 1000);
            
            if (instance != null)
                instance.RpcShowRoundSummary(instance.classlistStart, newList, leadingTeam, RoundSummary.EscapedClassD, RoundSummary.EscapedScientists, RoundSummary.KilledBySCPs, num5, (int)RoundStart.RoundLength.TotalSeconds);
            
            yield return Timing.WaitForSeconds(num5 - 1);
            instance.RpcDimScreen();
            yield return Timing.WaitForSeconds(1f);
            RoundRestart.InitiateRoundRestart();
        }
    }
}