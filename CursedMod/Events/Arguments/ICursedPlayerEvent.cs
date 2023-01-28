// -----------------------------------------------------------------------
// <copyright file="ICursedPlayerEvent.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Player;

namespace CursedMod.Events.Arguments;

public interface ICursedPlayerEvent
{
    public CursedPlayer Player { get; }
}