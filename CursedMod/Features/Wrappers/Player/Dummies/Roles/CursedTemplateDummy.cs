using System;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.PlayableScps.Scp049;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class CursedTemplateDummy : CursedDummy
    {
        public Scp049Role RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }

        internal CursedTemplateDummy(ReferenceHub hub) : base(hub)
        {
            SetRole(PlayerRoles.RoleTypeId.Scp049);
            RoleBase = hub.roleManager.CurrentRole as Scp049Role;

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out Scp049AttackAbility attackAbility))
                throw new InvalidOperationException("Ex Ability subroutine not found while initializing ex dummy.");
        }
    }
}
