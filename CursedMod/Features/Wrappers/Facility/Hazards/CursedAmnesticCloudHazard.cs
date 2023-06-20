// -----------------------------------------------------------------------
// <copyright file="CursedAmnesticCloudHazard.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Enums;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedAmnesticCloudHazard : CursedTemporaryHazard
{
    internal CursedAmnesticCloudHazard(Scp939AmnesticCloudInstance hazard)
        : base(hazard)
    {
        Base = hazard;
        HazardType = EnvironmentalHazardType.AmnesticCloud;
    }
    
    public Scp939AmnesticCloudInstance Base { get; }
    
    public Scp939AmnesticCloudInstance.CloudState State
    {
        get => Base.State;
        set => Base.State = value;
    }

    public float NormalizedHoldTime => Base.NormalizedHoldTime;
    
    public Vector2 MinMaxTime => Base.MinMaxTime;

    public void PauseAllClouds() => Base.PauseAll();

    public void CreateCloud(CursedPlayer player) => Base.ServerSetup(player.ReferenceHub);
}