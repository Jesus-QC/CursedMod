// -----------------------------------------------------------------------
// <copyright file="PermissionsGroup.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace CursedMod.Loader.Permissions;

public class PermissionsGroup
{
    [YamlIgnore]
    public HashSet<string> SpecialPermissions { get; set; } = new ();

    public string[] InheritedGroups { get; set; } = Array.Empty<string>();

    public string[] Permissions { get; set; } = Array.Empty<string>();
}