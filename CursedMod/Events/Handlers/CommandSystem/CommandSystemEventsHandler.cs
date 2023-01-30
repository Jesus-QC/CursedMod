// -----------------------------------------------------------------------
// <copyright file="CommandSystemEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.CommandSystem;

namespace CursedMod.Events.Handlers.CommandSystem;

public static class CommandSystemEventsHandler
{
    public static event EventManager.CursedEventHandler<ExecutingRemoteAdminCommandEventArgs> ExecutingRemoteAdminCommand;

    public static void OnExecutingRemoteAdminCommand(ExecutingRemoteAdminCommandEventArgs args)
    {
        ExecutingRemoteAdminCommand.InvokeEvent(args);
    }
}