// -----------------------------------------------------------------------
// <copyright file="IssuingBanEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace CursedMod.Events.Arguments.BanSystem;

public class IssuingBanEventArgs : EventArgs, ICursedCancellableEvent
{
    public IssuingBanEventArgs(BanDetails banDetails, BanHandler.BanType banType)
    {
        IsAllowed = true;
        BanDetails = banDetails;
        BanType = banType;
    }
    
    public bool IsAllowed { get; set; }
    
    public BanDetails BanDetails { get; set; }
    
    public BanHandler.BanType BanType { get; set; }
}