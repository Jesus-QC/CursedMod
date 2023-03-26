// -----------------------------------------------------------------------
// <copyright file="ModulePriority.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CursedMod.Loader.Modules.Enums;

public enum ModulePriority : byte
{
    Core = 255,
    VeryHigh = 200,
    High = 150,
    Medium = 100,
    Low = 50,
    VeryLow = 15,
}