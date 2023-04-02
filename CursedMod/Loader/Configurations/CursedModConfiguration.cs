﻿// -----------------------------------------------------------------------
// <copyright file="CursedModConfiguration.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;

namespace CursedMod.Loader.Configurations;

public class CursedModConfiguration
{
    public bool LoadCursedMod { get; set; } = true;

    [Description("Whether or not CursedMod version is shown in the server name")]
    public bool ShowCursedModVersion { get; set; } = true;
}