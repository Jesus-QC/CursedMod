using System;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class Cursed049Dummy : CursedDummy
    {
        public Scp049Role RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }
        public Scp049AttackAbility AttackSubroutine { get; }
        public Scp049ResurrectAbility ResurrectSubroutine { get; }
        public Scp049SenseAbility SenseSubroutine { get; }
        public Scp049CallAbility CallSubroutine { get; }

        internal Cursed049Dummy(ReferenceHub hub) : base(hub)
        {
            SetRole(PlayerRoles.RoleTypeId.Scp049);
            RoleBase = hub.roleManager.CurrentRole as Scp049Role;

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out Scp049AttackAbility attackAbility))
                throw new InvalidOperationException("AttackAbility subroutine not found while initializing 049 dummy.");
            AttackSubroutine = attackAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp049ResurrectAbility resurrectAbility))
                throw new InvalidOperationException("ResurrectAbility subroutine not found while initializing 049 dummy.");
            ResurrectSubroutine = resurrectAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp049SenseAbility senseAbility))
                throw new InvalidOperationException("SenseAbility subroutine not found while initializing 049 dummy.");
            SenseSubroutine = senseAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp049CallAbility callAbility))
                throw new InvalidOperationException("CallAbility subroutine not found while initializing 049 dummy.");
            CallSubroutine = callAbility;
        }

        public CursedPlayer Sense(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                SenseSubroutine.Cooldown.Remaining = 0;

            SenseSubroutine.ServerSendRpc(true);
            return CursedPlayer.Get(SenseSubroutine.Target);
        }

        public void Attack(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                AttackSubroutine.Cooldown.Remaining = 0;

            AttackSubroutine.ServerSendRpc(true);
        }

        public void Resurrect()
        {
            ResurrectSubroutine.ServerSendRpc(true); // this prob doesnt work, need to check it out later
        }

        public void Call(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                CallSubroutine.Cooldown.Remaining = 0;

            CallSubroutine.ServerSendRpc(true);
        }
    }
}
