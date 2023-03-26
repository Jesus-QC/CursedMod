// -----------------------------------------------------------------------
// <copyright file="CursedPryableDoor.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Enums;
using Interactables.Interobjects;

namespace CursedMod.Features.Wrappers.Facility.Doors;

public class CursedPryableDoor : CursedDoor
{
    internal CursedPryableDoor(PryableDoor door)
        : base(door)
    {
        PryableBase = door;
        DoorType = DoorType.Pryable;
    }
    
    public PryableDoor PryableBase { get; }

    public float RemainingPryCooldown
    {
        get => PryableBase._remainingPryCooldown;
        set => PryableBase._remainingPryCooldown = value;
    }

    public float PryCooldown
    {
        get => PryableBase._pryAnimDuration;
        set => PryableBase._pryAnimDuration = value;
    }

    public bool IsScp106Passable => PryableBase.IsScp106Passable;
    
    public bool TryPry() => PryableBase.TryPryGate(ReferenceHub.HostHub);
}