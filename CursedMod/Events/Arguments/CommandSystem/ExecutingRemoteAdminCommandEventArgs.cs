// -----------------------------------------------------------------------
// <copyright file="ExecutingRemoteAdminCommandEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CommandSystem;

namespace CursedMod.Events.Arguments.CommandSystem;

public class ExecutingRemoteAdminCommandEventArgs : EventArgs, ICursedCancellableEvent
{
    public ExecutingRemoteAdminCommandEventArgs(ICommand command, string[] arguments, ICommandSender sender)
    {
        IsAllowed = true;
        Command = command;
        FullArguments = arguments;
        Sender = sender;
    }
    
    public bool IsAllowed { get; set; }

    public ICommand Command { get; set; }
    
    public string[] FullArguments { get; set; }

    public ArraySegment<string> Arguments => FullArguments.Segment(1);
    
    public ICommandSender Sender { get; set; }
}