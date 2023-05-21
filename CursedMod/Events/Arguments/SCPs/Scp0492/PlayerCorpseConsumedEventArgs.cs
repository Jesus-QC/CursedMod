// -----------------------------------------------------------------------
// <copyright file="PlayerCorpseConsumedEventArgs.cs" company="CursedMod">
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

public class PlayerCorpseConsumedEventArgs : EventArgs, ICursedPlayerEvent, ICursedRagdollEvent
{
    public PlayerCorpseConsumedEventArgs(ZombieConsumeAbility consumeAbility)
    {
        Player = CursedPlayer.Get(consumeAbility.Owner);
        Ragdoll = CursedRagdoll.Get(consumeAbility.CurRagdoll);
    }
    
    public CursedPlayer Player { get; }

    public CursedRagdoll Ragdoll { get; }
}