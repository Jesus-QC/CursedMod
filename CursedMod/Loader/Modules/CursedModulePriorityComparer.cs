// -----------------------------------------------------------------------
// <copyright file="CursedModulePriorityComparer.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace CursedMod.Loader.Modules;

public class CursedModulePriorityComparer : IComparer<ICursedModule>
{
    public int Compare(ICursedModule x, ICursedModule y)
    {
        int value = y!.LoadPriority.CompareTo(x!.LoadPriority);
       
        if (value == 0)
            value = x.GetHashCode().CompareTo(y.GetHashCode());

        return value == 0 ? 1 : value;
    }
}