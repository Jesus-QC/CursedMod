// -----------------------------------------------------------------------
// <copyright file="CursedPocketDimensionExit.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CursedMod.Features.Wrappers.Facility.PocketDimension;

public class CursedPocketDimensionExit
{
    internal CursedPocketDimensionExit(PocketDimensionTeleport dimensionTeleport)
    {
        Base = dimensionTeleport;
    }
    
    public PocketDimensionTeleport Base { get; }

    public PocketDimensionTeleport.PDTeleportType ExitType
    {
        get => Base._type;
        set => Base._type = value;
    }

    public static CursedPocketDimensionExit Get(PocketDimensionTeleport pocketDimensionTeleport) => new (pocketDimensionTeleport);
}