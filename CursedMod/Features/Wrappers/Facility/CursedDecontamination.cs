using CursedMod.Features.Wrappers.Player;
using LightContainmentZoneDecontamination;
using PhaseFunction = LightContainmentZoneDecontamination.DecontaminationController.DecontaminationPhase.PhaseFunction;
using DecontaminationStatus = LightContainmentZoneDecontamination.DecontaminationController.DecontaminationStatus;

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

    public static void StopDecontamination() => Status = DecontaminationStatus.Disabled;

    public static void RestartDecontamination()
    {
        DecontaminationController.NetworkRoundStartTime = 0;
        Status = DecontaminationStatus.None;
    }

    public static void ForceDecontamination() => DecontaminationController.ForceDecontamination();

    public static void DisableElevators() => DecontaminationController.DisableElevators();

    public static bool IsAudibleForPlayer(CursedPlayer player) => DecontaminationController.IsAudibleForClient(player.ReferenceHub);

    public static void SetStatus(DecontaminationStatus status) => DecontaminationController.Singleton.DecontaminationOverride = status;
}