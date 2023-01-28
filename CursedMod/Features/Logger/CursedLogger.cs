using System.Diagnostics;
using PluginAPI.Core;

namespace CursedMod.Features.Logger;

public static class CursedLogger
{
    [Conditional("DEBUG")]
    internal static void InternalDebug(object obj)
    {
        Log.Debug(obj.ToString());
    }
}