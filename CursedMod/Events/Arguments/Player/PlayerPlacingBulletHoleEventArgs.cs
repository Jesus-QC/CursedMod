// -----------------------------------------------------------------------
// <copyright file="PlayerPlacingBulletHoleEventArgs.cs" company="CursedMod">
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

public class PlayerPlacingBulletHoleEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerPlacingBulletHoleEventArgs(StandardHitregBase hitReg, RaycastHit raycastHit)
    {
        Player = CursedPlayer.Get(hitReg.Hub);
        RaycastHit = raycastHit;
        Position = raycastHit.point;
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }
    
    public RaycastHit RaycastHit { get; }
    
    public Vector3 Position { get; }

    public bool IsAllowed { get; set; }
}