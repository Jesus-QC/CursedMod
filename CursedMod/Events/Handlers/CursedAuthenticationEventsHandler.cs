// -----------------------------------------------------------------------
// <copyright file="CursedAuthenticationEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Authentication;

namespace CursedMod.Events.Handlers;

public static class CursedAuthenticationEventsHandler
{
    public static event EventManager.CursedEventHandler<CheckingReservedSlotEventArgs> CheckingReservedSlot;

    internal static void OnCheckingReservedSlot(CheckingReservedSlotEventArgs args)
    {
        CheckingReservedSlot.InvokeEvent(args);
    }
}