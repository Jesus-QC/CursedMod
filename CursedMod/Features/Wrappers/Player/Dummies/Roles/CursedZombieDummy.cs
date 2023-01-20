using System;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.PlayableScps.Scp049.Zombies;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class CursedZombieDummy : CursedDummy
    {
        public ZombieRole RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }
        public ZombieAttackAbility AttackSubroutine { get; }
        public ZombieBloodlustAbility BloodlustSubroutine { get; }
        public ZombieConsumeAbility ConsumeSubroutine { get; }

        internal CursedZombieDummy(ReferenceHub hub) : base(hub)
        {
            SetRole(PlayerRoles.RoleTypeId.Scp0492);
            RoleBase = hub.roleManager.CurrentRole as ZombieRole;

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out ZombieAttackAbility attackAbility))
                throw new InvalidOperationException("AttackAbility subroutine not found while initializing 049-2 dummy.");
            AttackSubroutine = attackAbility;

            if (!SubroutineManager.TryGetSubroutine(out ZombieBloodlustAbility bloodlustAbility))
                throw new InvalidOperationException("BloodlustAbility subroutine not found while initializing 049-2 dummy.");
            BloodlustSubroutine = bloodlustAbility;

            if (!SubroutineManager.TryGetSubroutine(out ZombieConsumeAbility consumeAbility))
                throw new InvalidOperationException("ConsumeAbility subroutine not found while initializing 049-2 dummy.");
            ConsumeSubroutine = consumeAbility;
        }

        public void ConsumeRagdoll()
        {
            ConsumeSubroutine.ServerSendRpc(true); // prob doesnt work, need to check later
        }

        public void Attack(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                AttackSubroutine.Cooldown.Remaining = 0;

            AttackSubroutine.ServerSendRpc(true);
        }
    }
}
