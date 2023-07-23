// -----------------------------------------------------------------------
// <copyright file="Scp0492ConsumingCorpseEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using CursedMod.Features.Wrappers.Player.Ragdolls;
using PlayerRoles.PlayableScps.Scp049.Zombies;

namespace CursedMod.Events.Arguments.SCPs.Scp0492;

public class Scp0492ConsumingCorpseEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedRagdollEvent
{
    public Scp0492ConsumingCorpseEventArgs(ZombieConsumeAbility consumeAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(consumeAbility.Owner);
        Ragdoll = CursedRagdoll.Get(consumeAbility.CurRagdoll);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public CursedRagdoll Ragdoll { get; }
}