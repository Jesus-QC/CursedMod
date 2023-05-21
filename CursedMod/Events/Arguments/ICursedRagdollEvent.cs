// -----------------------------------------------------------------------
// <copyright file="ICursedRagdollEvent.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player.Ragdolls;

namespace CursedMod.Events.Arguments;

public interface ICursedRagdollEvent
{
    public CursedRagdoll Ragdoll { get; }
}