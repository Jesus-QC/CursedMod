// -----------------------------------------------------------------------
// <copyright file="ICursedItemEvent.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Features.Wrappers.Inventory.Items;

namespace CursedMod.Events.Arguments;

public interface ICursedItemEvent
{
    public CursedItem Item { get; }
}