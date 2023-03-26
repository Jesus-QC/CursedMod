// -----------------------------------------------------------------------
// <copyright file="ICursedPickupEvent.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Inventory.Pickups;

namespace CursedMod.Events.Arguments;

public interface ICursedPickupEvent
{
    public CursedPickup Pickup { get; }
}