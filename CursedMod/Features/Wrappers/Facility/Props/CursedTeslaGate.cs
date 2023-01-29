// -----------------------------------------------------------------------
// <copyright file="CursedTeslaGate.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

public class CursedTeslaGate
{
    internal CursedTeslaGate(TeslaGate gate)
    {
        Base = gate;
    }
    
    public TeslaGate Base { get; }
    
    public float TriggerRange
    {
        get => Base.sizeOfTrigger;
        set => Base.sizeOfTrigger = value;
    }

    public bool IsInRange(Vector3 position) => Base.InRange(position);

    public void StartIdling() => Base.RpcDoIdle();

    public void StopIdling() => Base.RpcDoneIdling();

    public void InstantBurst() => Base.RpcInstantBurst();

    public void PlayAnimation() => Base.RpcPlayAnimation();
}