// -----------------------------------------------------------------------
// <copyright file="CursedCommandManager.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using CursedMod.Features.Logger;
using RemoteAdmin;

namespace CursedMod.Loader.Commands;

public static class CursedCommandManager
{
    public static ICommand RegisterCommand(CursedCommandType commandHandlerType, Type commandType)
    {
        if (Activator.CreateInstance(commandType) is not ICommand command)
        {
            CursedLogger.LogError($"There was an issue registering the command {commandType.FullName}. The object is null.");
            return null;
        }

        switch (commandHandlerType)
        {
            case CursedCommandType.GameConsole:
                GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(command);
                break;
            case CursedCommandType.RemoteAdmin:
                CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(command);
                break;
            case CursedCommandType.Client:
                QueryProcessor.DotCommandHandler.RegisterCommand(command);
                break;
        }

        return command;
    }
}