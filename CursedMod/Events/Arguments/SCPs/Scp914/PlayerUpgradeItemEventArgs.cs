// -----------------------------------------------------------------------
// <copyright file="PlayerUpgradeItemEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CursedMod.Features.Wrappers.Inventory.Pickups;
using CursedMod.Features.Wrappers.Player;
using Scp914;
using UnityEngine;

namespace CursedMod.Events.Arguments.SCPs.Scp914;

public class PlayerUpgradeItemEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedPickupEvent
{
    public PlayerUpgradeItemEventArgs(CursedPlayer player, CursedPickup pickup, List<CursedPickup> pickups, Vector3 outputPosition, Scp914KnobSetting knobSetting)
    {
        IsAllowed = true;
        Player = player;
        Pickup = pickup;
        PickupsToUpgrade = pickups;
        OutputPosition = outputPosition;
        KnobSetting = knobSetting;
    }

    public bool IsAllowed { get; set; }

    public CursedPickup Pickup { get; }
    
    public List<CursedPickup> PickupsToUpgrade { get; }

    public CursedPlayer Player { get; }
    
    public Vector3 OutputPosition { get; }
    
    public Scp914KnobSetting KnobSetting { get; }
}