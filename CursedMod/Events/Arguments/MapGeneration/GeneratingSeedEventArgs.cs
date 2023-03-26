// -----------------------------------------------------------------------
// <copyright file="GeneratingSeedEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace CursedMod.Events.Arguments.MapGeneration;

public class GeneratingSeedEventArgs : EventArgs
{
    public GeneratingSeedEventArgs(int seed)
    {
        Seed = seed;
    }

    public int Seed { get; set; }
}