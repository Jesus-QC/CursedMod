// -----------------------------------------------------------------------
// <copyright file="CursedLogger.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using PluginAPI.Core;

namespace CursedMod.Features.Logger;

public static class CursedLogger
{
    [Conditional("DEBUG")]
    internal static void InternalDebug(object obj)
    {
        Log.Debug(obj.ToString());
    }
}