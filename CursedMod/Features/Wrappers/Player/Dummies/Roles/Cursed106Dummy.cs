using System;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.PlayableScps.Scp106;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class Cursed106Dummy : CursedDummy
    {
        public Scp106Role RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }
        public Scp106Attack AttackSubroutine { get; }
        public Scp106HuntersAtlasAbility HuntersAtlasSubroutine { get; }
        public Scp106StalkAbility StalkSubroutine { get; }
        public Scp106SinkholeController SinkholeController { get; }
        public Scp106Vigor Vigor { get; }

        internal Cursed106Dummy(ReferenceHub hub) : base(hub)
        {
            SetRole(PlayerRoles.RoleTypeId.Scp106);
            RoleBase = hub.roleManager.CurrentRole as Scp106Role;

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out Scp106Attack attackAbility))
                throw new InvalidOperationException("AttackAbility subroutine not found while initializing 106 dummy.");
            AttackSubroutine = attackAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp106HuntersAtlasAbility huntersAtlasAbility))
                throw new InvalidOperationException("HuntersAtlasAbility subroutine not found while initializing 106 dummy.");
            HuntersAtlasSubroutine = huntersAtlasAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp106StalkAbility stalkAbility))
                throw new InvalidOperationException("StalkAbility subroutine not found while initializing 106 dummy.");
            StalkSubroutine = stalkAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp106SinkholeController sinkholeController))
                throw new InvalidOperationException("SinkholeController subroutine not found while initializing 106 dummy.");
            SinkholeController = sinkholeController;

            if (!SubroutineManager.TryGetSubroutine(out Scp106Vigor vigor))
                throw new InvalidOperationException("Vigor subroutine not found while initializing 106 dummy.");
            Vigor = vigor;
        }

        public void ToggleStalk(bool overrideCooldown = true)
        {
            if (overrideCooldown)
            {
                SinkholeController.Cooldown.Remaining = 0;
                Vigor.VigorAmount = 100;
            }

            StalkSubroutine.ServerSendRpc(true);
        }

        public void Attack(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                AttackSubroutine._nextAttack = 0;

            AttackSubroutine.ServerSendRpc(true);
        }

        // TODO: add hunters atlas
    }
}
