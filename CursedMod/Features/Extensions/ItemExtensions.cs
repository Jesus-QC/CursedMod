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

    public static bool IsWeapon(this ItemType itemType)
    {
        return itemType is ItemType.GunCom45 or ItemType.GunCrossvec or ItemType.GunLogicer or ItemType.GunRevolver
            or ItemType.GunShotgun or ItemType.GunAK or ItemType.GunCOM15 or ItemType.GunCOM18 or ItemType.GunE11SR
            or ItemType.GunFSP9;
    }

    public static bool IsScpItem(this ItemType itemType)
    {
        return itemType is ItemType.SCP018 or ItemType.SCP207 or ItemType.SCP244a or ItemType.SCP244b or ItemType.SCP268
            or ItemType.SCP330 or ItemType.SCP500 or ItemType.SCP1576 or ItemType.SCP1576 or ItemType.SCP1853
            or ItemType.SCP2176;
    }
}