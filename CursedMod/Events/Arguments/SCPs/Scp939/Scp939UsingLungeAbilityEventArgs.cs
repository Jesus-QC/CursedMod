// -----------------------------------------------------------------------
// <copyright file="Scp939UsingLungeAbilityEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;

namespace CursedMod.Events.Arguments.SCPs.Scp939;

public class Scp939UsingLungeAbilityEventArgs : EventArgs, ICursedPlayerEvent
{
    public Scp939UsingLungeAbilityEventArgs(Scp939LungeAbility lungeAbility)
    {
        Player = CursedPlayer.Get(lungeAbility.Owner);
    }

    public CursedPlayer Player { get; }
}