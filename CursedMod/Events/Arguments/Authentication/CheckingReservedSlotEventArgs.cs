// -----------------------------------------------------------------------
// <copyright file="CheckingReservedSlotEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using PluginAPI.Events;

namespace CursedMod.Events.Arguments.Authentication;

public class CheckingReservedSlotEventArgs : EventArgs
{
    public CheckingReservedSlotEventArgs(string userId, bool hasReservedSlot, PlayerCheckReservedSlotCancellationData reservedSlotCancellationData)
    {
        UserId = userId;
        HasReservedSlot = hasReservedSlot;
        CheckReservedSlotCancellationData = reservedSlotCancellationData;
    }
    
    public PlayerCheckReservedSlotCancellationData CheckReservedSlotCancellationData { get; set; }
    
    public string UserId { get; }
    
    public bool HasReservedSlot { get; set; }
}