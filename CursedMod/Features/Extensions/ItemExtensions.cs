// -----------------------------------------------------------------------
// <copyright file="ItemExtensions.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CursedMod.Features.Extensions;

public static class ItemExtensions
{
    public static bool IsKeyCard(this ItemType itemType)
    {
        int i = (int)itemType;
        return i is > -1 and < 12;
    }
}