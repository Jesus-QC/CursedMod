using PlayerRoles.PlayableScps.Scp939;
using System;
using System.Collections.Generic;
using PlayerRoles.PlayableScps.Subroutines;
using MEC;

namespace CursedMod.Features.Wrappers.Player.Dummies.Roles
{
    public class Cursed939Dummy : CursedDummy
    {
        public Scp939Role RoleBase { get; }
        public SubroutineManagerModule SubroutineManager { get; }
        public Scp939LungeAbility LungeSubroutine { get; }
        public Scp939FocusAbility FocusSubroutine { get; }
        public Scp939AmnesticCloudAbility AmnesticCloudSubroutine { get; }
        public Scp939ClawAbility ClawSubroutine { get; }

        internal Cursed939Dummy(ReferenceHub hub) : base(hub)
        {
            if (hub.roleManager.CurrentRole is not Scp939Role RoleBase)
                throw new InvalidOperationException("Cursed939Dummy should only be applied to 939 dummies");

            SubroutineManager = RoleBase.SubroutineModule;

            if (!SubroutineManager.TryGetSubroutine(out Scp939LungeAbility lungeAbility))
                throw new InvalidOperationException("LungeAbility subroutine not found while initializing 939 dummy.");
            LungeSubroutine = lungeAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp939FocusAbility focusAbility))
                throw new InvalidOperationException("FocusAbility subroutine not found while initializing 939 dummy.");
            FocusSubroutine = focusAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp939AmnesticCloudAbility amnesiaAbility))
                throw new InvalidOperationException("AmnesticCloudAbility subroutine not found while initializing 939 dummy.");
            AmnesticCloudSubroutine = amnesiaAbility;

            if (!SubroutineManager.TryGetSubroutine(out Scp939ClawAbility clawAbility))
                throw new InvalidOperationException("ClawAbility subroutine not found while initializing 939 dummy.");
            ClawSubroutine = clawAbility;
        }

        public Scp939LungeState LungeState { get => LungeSubroutine.State; } // TODO ADD SETTER
        public bool CanLunge => LungeSubroutine.IsReady;
        public bool CanFocus => FocusSubroutine.IsAvailable;
        public bool IsGoingIntoFocus => FocusSubroutine.State > 0 && FocusSubroutine.State < 1;
        public bool IsInFocus => IsGoingIntoFocus || IsFocused;
        public bool IsFocused => FocusSubroutine.State == 1;
        public Scp939AmnesticCloudInstance CloudInstance => AmnesticCloudSubroutine._instancePrefab;
        public Scp939AmnesticCloudInstance.CloudState CloudState => CloudInstance.State;

        public void Lunge()
        {
            if (LungeState != Scp939LungeState.None) // ply already lunging
                return;

            Timing.RunCoroutine(LungeInternal());
        }

        public void Focus()
        {
            if (IsInFocus)
                return;

            FocusSubroutine.ServerSendRpc(true);
        }

        public void DeployCloud(bool overrideCooldown = true)
        {
            if (CloudState != Scp939AmnesticCloudInstance.CloudState.Destroyed)
                return;

            if (overrideCooldown)
                AmnesticCloudSubroutine.Cooldown.Remaining = 0;

            AmnesticCloudSubroutine.ServerSendRpc(true);
        }

        public void Claw()
        {
            if (ClawSubroutine.CanTriggerAbility)
                return;

            ClawSubroutine.ServerSendRpc(true);
        }

        private IEnumerator<float> LungeInternal()
        {
            if (!IsInFocus)
                Focus();

            yield return Timing.WaitUntilTrue(() => CanLunge);
            LungeSubroutine.ServerSendRpc(true);
        }
    }
}
