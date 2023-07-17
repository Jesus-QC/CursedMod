// -----------------------------------------------------------------------
// <copyright file="PlayerDeactivatingGeneratorEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Props;
using CursedMod.Features.Wrappers.Player;
using MapGeneration.Distributors;

namespace CursedMod.Events.Arguments.Facility.Generators;

public class PlayerDeactivatingGeneratorEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent, ICursedGeneratorEvent
{
    public PlayerDeactivatingGeneratorEventArgs(ReferenceHub player, Scp079Generator generator)
    {
        Player = CursedPlayer.Get(player);
        Generator = CursedGenerator.Get(generator);
        IsAllowed = true;
    }
    
    public CursedPlayer Player { get; }

    public CursedGenerator Generator { get; }

    public bool IsAllowed { get; set; }
}