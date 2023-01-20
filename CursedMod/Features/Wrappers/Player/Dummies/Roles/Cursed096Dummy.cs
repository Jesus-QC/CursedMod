using System;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.PlayableScps.Scp096;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class Cursed096Dummy : CursedDummy
    {
        public Scp096Role RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }
        public Scp096PrygateAbility PrygateSubroutine { get; }
        public Scp096AttackAbility AttackSubroutine { get; }
        public Scp096TryNotToCryAbility TryNotToCrySubroutine { get; }
        public Scp096ChargeAbility ChargeSubroutine { get; }
        public Scp096RageCycleAbility RageCycleSubroutine { get; }
        public Scp096StateController StateController { get; }

        internal Cursed096Dummy(ReferenceHub hub) : base(hub)
        {
            SetRole(PlayerRoles.RoleTypeId.Scp096);
            RoleBase = hub.roleManager.CurrentRole as Scp096Role;

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out Scp096PrygateAbility prygateAbility))
                throw new InvalidOperationException("PrygateAbility subroutine not found while initializing 096 dummy.");
            PrygateSubroutine = prygateAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp096AttackAbility attackAbility))
                throw new InvalidOperationException("AttackAbility subroutine not found while initializing 096 dummy.");
            AttackSubroutine = attackAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp096TryNotToCryAbility tryNotToCryAbility))
                throw new InvalidOperationException("TryNotToCryAbility subroutine not found while initializing 096 dummy.");
            TryNotToCrySubroutine = tryNotToCryAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp096ChargeAbility chargeAbility))
                throw new InvalidOperationException("ChargeAbility subroutine not found while initializing 096 dummy.");
            ChargeSubroutine = chargeAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp096RageCycleAbility rageCycleAbility))
                throw new InvalidOperationException("RageCycleAbility subroutine not found while initializing 096 dummy.");
            RageCycleSubroutine = rageCycleAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp096StateController stateController))
                throw new InvalidOperationException("StateController subroutine not found while initializing 096 dummy.");
            StateController = stateController;
        }

        public void Stare(float duration = 10f)
            => RageCycleSubroutine.ServerTryEnableInput(duration);

        public void Enrage(bool forceStare = true)
        {
            if (!RageCycleSubroutine._activationTime.IsReady && forceStare)
                Stare();

            StateController.SetRageState(Scp096RageState.Enraged);
        }

        public void Calm()
            => StateController.SetRageState(Scp096RageState.Calming);

        public void PryGate()
            => PrygateSubroutine.ServerSendRpc(true);

        public void Charge()
            => ChargeSubroutine.ServerSendRpc(true);

        public void Attack()
        {
            if (!AttackSubroutine._serverAttackCooldown.IsReady)
                return;

            AttackSubroutine.ServerSendRpc(true);
        }

        public void TryNotToCry()
        {
            if (TryNotToCrySubroutine.IsActive)
                return;

            TryNotToCrySubroutine.ServerSendRpc(true);
        }
    }
}
