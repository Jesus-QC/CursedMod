// -----------------------------------------------------------------------
// <copyright file="Scp096TryingNotToCryEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp096;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp096;

public class Scp096TryingNotToCryEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp096TryingNotToCryEventArgs(Scp096TryNotToCryAbility tryNotToCryAbility)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(tryNotToCryAbility.Owner);
        Object = Physics.Raycast(Player.PlayerCameraReference.position, Player.PlayerCameraReference.forward, out RaycastHit hit, 1f)
            ? hit.collider.gameObject
            : null;
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }

    public GameObject Object { get; }
}