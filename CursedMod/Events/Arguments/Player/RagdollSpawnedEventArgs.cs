// -----------------------------------------------------------------------
// <copyright file="RagdollSpawnedEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player.Ragdolls;

namespace CursedMod.Events.Arguments.Player;

public class RagdollSpawnedEventArgs : EventArgs
{
    public RagdollSpawnedEventArgs(BasicRagdoll ragdoll)
    {
        Ragdoll = CursedRagdoll.Get(ragdoll);
    }
    
    public CursedRagdoll Ragdoll { get; }
}