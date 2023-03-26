// -----------------------------------------------------------------------
// <copyright file="CursedScp096Role.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Subroutines;

namespace CursedMod.Features.Wrappers.Player.Roles.SCPs;

public class CursedScp096Role : CursedFpcRole
{
    internal CursedScp096Role(Scp096Role roleBase)
        : base(roleBase)
    {
        ScpRoleBase = roleBase;

        if (SubroutineModule.TryGetSubroutine(out Scp096ChargeAbility chargeAbility))
            ChargeAbility = chargeAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp096PrygateAbility prygate))
            PrygateAbility = prygate;
        if (SubroutineModule.TryGetSubroutine(out Scp096RageCycleAbility rageCycleAbility))
            RageCycleAbility = rageCycleAbility;
        if (SubroutineModule.TryGetSubroutine(out Scp096TargetsTracker targetsTracker))
            TargetsTracker = targetsTracker;
        if (SubroutineModule.TryGetSubroutine(out Scp096TryNotToCryAbility tryNotToCryAbility)) 
            TryNotToCryAbility = tryNotToCryAbility;
    }

    public Scp096Role ScpRoleBase { get; }
    
    public Scp096ChargeAbility ChargeAbility { get; }
    
    public Scp096PrygateAbility PrygateAbility { get; }
    
    public Scp096RageCycleAbility RageCycleAbility { get; }

    public Scp096TargetsTracker TargetsTracker { get; }
    
    public Scp096TryNotToCryAbility TryNotToCryAbility { get; }

    public HumeShieldModuleBase HumeShieldModule
    {
        get => ScpRoleBase.HumeShieldModule;
        set => ScpRoleBase.HumeShieldModule = value;
    }

    public SubroutineManagerModule SubroutineModule
    {
        get => ScpRoleBase.SubroutineModule;
        set => ScpRoleBase.SubroutineModule = value;
    }

    public Scp096StateController StateController
    {
        get => ScpRoleBase.StateController;
        set => ScpRoleBase.StateController = value;
    }

    public void SetState(Scp096RageState state) => StateController.SetRageState(state);

    public void Enrage() => StateController.SetRageState(Scp096RageState.Enraged);

    public void EscapeEnrage() => SetState(Scp096RageState.Distressed);

    public void SetAbilityState(Scp096AbilityState abilityState) => StateController.SetAbilityState(abilityState);

    public void ResetAbilityState() => StateController.SetAbilityState(Scp096AbilityState.None);

    public void Charge(float cooldown = 1f)
    {
        ChargeAbility._hitHandler.Clear();
        ChargeAbility.Duration.Trigger(cooldown);
        ChargeAbility.ScpRole.StateController.SetAbilityState(Scp096AbilityState.Charging);
        ChargeAbility.ServerSendRpc(true);
    }

    public void EnableRageInput(float duration = 10f) => RageCycleAbility.ServerTryEnableInput(duration);

    public void AddTarget(CursedPlayer target, bool isForLook) => TargetsTracker.AddTarget(target.ReferenceHub, isForLook);

    public void RemoveTarget(CursedPlayer target) => TargetsTracker.RemoveTarget(target.ReferenceHub);

    public void ClearAllTargets() => TargetsTracker.ClearAllTargets();

    public void SetTryNotToCry(bool active) => TryNotToCryAbility.IsActive = active;
}