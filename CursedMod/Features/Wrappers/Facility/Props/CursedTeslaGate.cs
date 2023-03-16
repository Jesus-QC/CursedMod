// -----------------------------------------------------------------------
// <copyright file="CursedTeslaGate.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using CursedMod.Features.Wrappers.Facility.Hazards;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedTeslaGate
{
    public static readonly Dictionary<TeslaGate, CursedTeslaGate> Dictionary = new ();
    
    private CursedTeslaGate(TeslaGate gate)
    {
        Base = gate;
        Dictionary.Add(gate, this);
    }
    
    public TeslaGate Base { get; }
    
    public float TriggerRange
    {
        get => Base.sizeOfTrigger;
        set => Base.sizeOfTrigger = value;
    }

    public static CursedTeslaGate Get(TeslaGate teslaGate) => Dictionary.ContainsKey(teslaGate) ? Dictionary[teslaGate] : new CursedTeslaGate(teslaGate);

    public bool IsInRange(Vector3 position) => Base.InRange(position);

    public void StartIdling() => Base.RpcDoIdle();

    public void StopIdling() => Base.RpcDoneIdling();

    public void InstantBurst() => Base.RpcInstantBurst();

    public void PlayAnimation() => Base.RpcPlayAnimation();
}