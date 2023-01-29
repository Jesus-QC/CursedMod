// -----------------------------------------------------------------------
// <copyright file="CursedDecontamination.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player;
using LightContainmentZoneDecontamination;
using DecontaminationStatus = LightContainmentZoneDecontamination.DecontaminationController.DecontaminationStatus;
using PhaseFunction = LightContainmentZoneDecontamination.DecontaminationController.DecontaminationPhase.PhaseFunction;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedDecontamination
{
    public static DecontaminationController DecontaminationController => DecontaminationController.Singleton;

    public static double StartTime
    {
        get => DecontaminationController.RoundStartTime;
        set => DecontaminationController.NetworkRoundStartTime = value;
    }

    public static double GetServerTime => DecontaminationController.GetServerTime;

    public static bool IsDecontaminating => DecontaminationController.IsDecontaminating;

    public static PhaseFunction CurrentFunction
    {
        get => DecontaminationController._curFunction;
        set => DecontaminationController._curFunction = value;
    }

    public static DecontaminationStatus Status
    {
        get => DecontaminationController.DecontaminationOverride;
        set => DecontaminationController.NetworkDecontaminationOverride = value;
    }

    public static DecontaminationController.DecontaminationPhase NextPhase => DecontaminationController.DecontaminationPhases[DecontaminationController._nextPhase];

    public static bool IsAudibleForPlayer(CursedPlayer player) => DecontaminationController.IsAudibleForClient(player.ReferenceHub);

    public static void SetStatus(DecontaminationStatus status) => DecontaminationController.Singleton.DecontaminationOverride = status;
    
    public static void StopDecontamination() => Status = DecontaminationStatus.Disabled;

    public static void ResumeDecontamination() => Status = DecontaminationStatus.None;

    public static void RestartDecontamination()
    {
        DecontaminationController.NetworkRoundStartTime = 0;
        Status = DecontaminationStatus.None;
    }

    public static void ForceDecontamination() => DecontaminationController.ForceDecontamination();

    public static void DisableElevators() => DecontaminationController.DisableElevators();
}