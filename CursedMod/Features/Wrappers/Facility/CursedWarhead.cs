using CursedMod.Features.Wrappers.Player;
using Footprinting;
using PluginAPI.Core;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility;

public static class CursedWarhead
{
    private static AlphaWarheadController _warheadController;
    private static AlphaWarheadOutsitePanel _outsitePanel;

    public static AlphaWarheadController Controller => _warheadController ??= AlphaWarheadController.Singleton;
    public static AlphaWarheadOutsitePanel OutsitePanel => _outsitePanel ??= Object.FindObjectOfType<AlphaWarheadOutsitePanel>();

    public static bool OpenDoors
    {
        get => Controller._openDoors;
        set => Controller._openDoors = value;
    }

    public static int Cooldown
    {
        get => Controller._cooldown;
        set => Controller._cooldown = value;
    }

    public static bool IsAutomatic
    {
        get => Controller._isAutomatic;
        set => Controller._isAutomatic = value;
    }

    public static bool AlreadyDetonated
    {
        get => Controller._alreadyDetonated;
        set => Controller._alreadyDetonated = value;
    }

    public static bool EnableAutoWarhead
    {
        get => Controller._autoDetonate;
        set => Controller._autoDetonate = value;
    }

    public static float AutoWarheadTime
    {
        get => Controller._autoDetonateTime;
        set => Controller._autoDetonateTime = value;
    }

    public static bool LockAutoWarhead
    {
        get => Controller._autoDetonateLock;
        set => Controller._autoDetonateLock = value;
    }

    public static bool IsLocked
    {
        get => Controller.IsLocked;
        set => Controller.IsLocked = value;
    }

    public static Footprint TriggeringPlayer
    {
        get => Controller._triggeringPlayer;
        set => Controller._triggeringPlayer = value;
    }

    public static AlphaWarheadController.DetonationScenario CurrentScenario => Controller.CurScenario;

    public static AlphaWarheadSyncInfo PreviousInfo
    {
        get => Controller._prevInfo;
        set => Controller._prevInfo = value;
    }

    public static AlphaWarheadSyncInfo Info
    {
        get => Controller.Info;
        set => Controller.NetworkInfo = value;
    }

    public static double CooldownEndTime
    {
        get => Controller.CooldownEndTime;
        set => Controller.NetworkCooldownEndTime = value;
    }

    public static int Kills
    {
        get => Controller.WarheadKills;
        set => Controller.WarheadKills = value;
    }

    public static CursedPlayer Activator => CursedPlayer.Get(TriggeringPlayer.Hub);

    public static bool Detonated => AlphaWarheadController.Detonated;

    public static bool InProgress = AlphaWarheadController.InProgress;

    public static float TimeUntilDetonation => AlphaWarheadController.TimeUntilDetonation;

    public static double StartTime = Info.StartTime;

    public static int ScenarioId => Info.ScenarioId;

    public static bool ResumeScenario => Info.ResumeScenario;

    public static void ForceTime(float remaining) => Controller.ForceTime(remaining);

    public static void InstantPrepare() => Controller.InstantPrepare();

    public static void StartDetonation(bool isAutomatic = false, bool suppressSubtitles = false, CursedPlayer trigger = null) => Controller.StartDetonation(isAutomatic, suppressSubtitles, trigger?.ReferenceHub);

    public static void CancelDetonation(CursedPlayer disabler = null) => Controller.CancelDetonation(disabler?.ReferenceHub);

    public static void Detonate() => Controller.Detonate();

    public static void Shake() => Warhead.Shake();

    public static bool LeverStatus
    {
        get => Warhead.LeverStatus;
        set => Warhead.LeverStatus = value;
    }

    public static bool OutsidePanelOpened
    {
        get => OutsitePanel.keycardEntered;
        set => OutsitePanel.NetworkkeycardEntered = value;
    }
}