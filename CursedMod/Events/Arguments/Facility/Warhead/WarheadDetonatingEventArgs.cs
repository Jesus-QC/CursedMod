// -----------------------------------------------------------------------
// <copyright file="WarheadDetonatingEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace CursedMod.Events.Arguments.Facility.Warhead;

public class WarheadDetonatingEventArgs : EventArgs, ICursedCancellableEvent
{
    public WarheadDetonatingEventArgs()
    {
        IsAllowed = true;
    }
    
    public bool IsAllowed { get; set; }
}