using PlayerRoles.PlayableScps.Scp173;
using System;
using PlayerRoles.PlayableScps.Subroutines;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class Cursed173Dummy : CursedDummy
    {
        public Scp173Role RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }
        public Scp173BlinkTimer BlinkTimer { get; }
        public Scp173TeleportAbility TeleportSubroutine { get; }
        public Scp173TantrumAbility TantrumSubroutine { get; }
        public Scp173BreakneckSpeedsAbility BreakneckSubroutine { get; }

        internal Cursed173Dummy(ReferenceHub hub) : base(hub)
        {
            if (hub.roleManager.CurrentRole is not Scp173Role RoleBase)
                throw new InvalidOperationException("Cursed173Dummy should only be applied to 173 dummies");

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out Scp173BlinkTimer blinkAbility))
                throw new InvalidOperationException("BlinkTimer subroutine not found while initializing 173 dummy.");
            BlinkTimer = blinkAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp173TantrumAbility tantrumAbility))
                throw new InvalidOperationException("Tantrum subroutine not found while initializing 173 dummy.");
            TantrumSubroutine = tantrumAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp173BreakneckSpeedsAbility breakneckAbility))
                throw new InvalidOperationException("BreakneckSpeeds subroutine not found while initializing 173 dummy.");
            BreakneckSubroutine = breakneckAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp173TeleportAbility teleportAbility))
                throw new InvalidOperationException("Teleport subroutine not found while initializing 173 dummy.");
            TeleportSubroutine = teleportAbility;
        }

        public void Blink()
            => TeleportSubroutine.ServerSendRpc(true);

        public void Blink(Vector3 position)
        {
            // uncomment when handlers are added
            // RotationVertical = GetRotationToBlink(position)
            Blink();
        }

        public void Tantrum(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                TantrumSubroutine.Cooldown.Remaining = 0;

            TantrumSubroutine.ServerSendRpc(true);
        }

        public void Breakneck(bool overrideCooldown = true)
        {
            if (overrideCooldown)
                BreakneckSubroutine.Cooldown.Remaining = 0;

            BreakneckSubroutine.ServerSendRpc(true);
        }

        public Quaternion GetRotationToBlink(Vector3 position)
            => Quaternion.LookRotation((position - Position).normalized);
    }
}
