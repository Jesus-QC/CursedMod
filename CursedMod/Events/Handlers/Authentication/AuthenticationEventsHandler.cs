// -----------------------------------------------------------------------
// <copyright file="AuthenticationEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Authentication;

namespace CursedMod.Events.Handlers.Authentication;

public static class AuthenticationEventsHandler
{
    public static event EventManager.CursedEventHandler<CheckingReservedSlotEventArgs> CheckingReservedSlot;

    public static void OnCheckingReservedSlot(CheckingReservedSlotEventArgs args)
    {
        CheckingReservedSlot.InvokeEvent(args);
    }
}