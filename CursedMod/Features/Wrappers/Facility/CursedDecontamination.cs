using LightContainmentZoneDecontamination;
using PhaseFunction = LightContainmentZoneDecontamination.DecontaminationController.DecontaminationPhase.PhaseFunction;
using DecontaminationStatus = LightContainmentZoneDecontamination.DecontaminationController.DecontaminationStatus;

namespace CursedMod.Features.Wrappers.Facility;

public class CursedDecontamination
{
    public DecontaminationController DecontaminationController { get; }

    public CursedDecontamination(DecontaminationController decontaminationController)
    {
        DecontaminationController = decontaminationController;
    }

    public double StartTime
    {
        get => DecontaminationController.NetworkRoundStartTime;
        set => DecontaminationController.NetworkRoundStartTime = value;
    }

    public bool IsDecontaminating => DecontaminationController.IsDecontaminating;
    
    public bool IsAnnouncementHearable => DecontaminationController.IsAnnouncementHearable;
    
    public PhaseFunction CurrentFunction => DecontaminationController.Singleton._curFunction;

    public DecontaminationStatus Status => DecontaminationController.Singleton.DecontaminationOverride;

    public void StopDecontamination() => DecontaminationController.NetworkRoundStartTime = -1f;
    
    public void StartDecontamination() => DecontaminationController.NetworkRoundStartTime = 1f;

    public void SetStatus(DecontaminationStatus status) => DecontaminationController.Singleton.DecontaminationOverride = status;
}