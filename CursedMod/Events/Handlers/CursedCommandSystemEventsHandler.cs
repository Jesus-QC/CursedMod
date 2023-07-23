// -----------------------------------------------------------------------
// <copyright file="CursedCommandSystemEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.CommandSystem;

namespace CursedMod.Events.Handlers;

public static class CursedCommandSystemEventsHandler
{
    public static event CursedEventManager.CursedEventHandler<ExecutingRemoteAdminCommandEventArgs> ExecutingRemoteAdminCommand;

    internal static void OnExecutingRemoteAdminCommand(ExecutingRemoteAdminCommandEventArgs args)
    {
        ExecutingRemoteAdminCommand.InvokeEvent(args);
    }
}