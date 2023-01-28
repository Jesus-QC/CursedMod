// -----------------------------------------------------------------------
// <copyright file="ICursedCancellableEvent.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CursedMod.Events.Arguments;

public interface ICursedCancellableEvent
{
    public bool IsAllowed { get; set; }
}