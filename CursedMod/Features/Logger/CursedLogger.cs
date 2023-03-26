// -----------------------------------------------------------------------
// <copyright file="CursedLogger.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Reflection;
using PluginAPI.Core;

namespace CursedMod.Features.Logger;

public static class CursedLogger
{
    public static void LogInformation(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        Log.Info(message.ToString(), prefix);
        
        // ServerConsole.AddLog(Log.FormatText($"\u001b[48;5;29;1m | INFO  \u001b[48;5;235m {prefix} \u001b[48;5;29m \x1b[0m\u001b[38;5;29m {message}"));
    }

    public static void LogCritical(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        Log.Error(message.ToString(), prefix);
        
        // ServerConsole.AddLog(Log.FormatText($"\u001b[48;5;160;1m | CRIT  \u001b[48;5;235m {prefix} \u001b[48;5;160m \x1b[0m\u001b[38;5;160m {message}"));
    }
    
    public static void LogDebug(object message, bool show = true, string prefix = null)
    {
        if (!show)
            return;
        
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        Log.Debug(message.ToString(), prefix);
        
        // ServerConsole.AddLog(Log.FormatText($"\u001b[48;5;104;1m | DEBUG \u001b[48;5;235m {prefix} \u001b[48;5;104m \x1b[0m\u001b[38;5;104m {message}"));
    }
    
    public static void LogError(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        Log.Error(message.ToString(), prefix);
        
        // ServerConsole.AddLog(Log.FormatText($"\u001b[48;5;197;1m | ERROR \u001b[48;5;235m {prefix} \u001b[48;5;197m \x1b[0m\u001b[38;5;197m {message}"));
    }
    
    public static void LogWarning(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        Log.Warning(message.ToString(), prefix);
        
        // ServerConsole.AddLog(Log.FormatText($"\u001b[48;5;208;1m | WARN  \u001b[48;5;235m {prefix} \u001b[48;5;208m \x1b[0m\u001b[38;5;208m {message}"));
    }
    
    internal static void InternalPrint(object obj)
    {
        Log.Debug(obj.ToString());
        
        // ServerConsole.AddLog(Log.FormatText($"\u001b[48;5;240;1m | .SYS  \u001b[48;5;235m CursedMod \u001b[48;5;240m \x1b[0m\u001b[38;5;240m {obj}"));
    }
    
    [Conditional("DEBUG")]
    internal static void InternalDebug(object obj)
    {
        LogDebug(obj, true, "CursedMod");
    }
}