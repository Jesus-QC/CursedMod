// -----------------------------------------------------------------------
// <copyright file="PlayerPlacingBloodDecalEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using InventorySystem.Items.Firearms.Modules;
using UnityEngine;

namespace CursedMod.Events.Arguments.Player;

public class PlayerPlacingBloodDecalEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerPlacingBloodDecalEventArgs(StandardHitregBase hitReg, ReferenceHub target, RaycastHit raycastHit)
    {
        Player = CursedPlayer.Get(hitReg.Hub);
        Target = CursedPlayer.Get(target);
        RaycastHit = raycastHit;
        Position = raycastHit.point;
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }
    
    public CursedPlayer Target { get; }
    
    public RaycastHit RaycastHit { get; }

    public Vector3 Position { get; }

    public bool IsAllowed { get; set; }
}