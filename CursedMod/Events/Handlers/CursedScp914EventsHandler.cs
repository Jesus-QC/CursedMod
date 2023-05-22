// -----------------------------------------------------------------------
// <copyright file="CursedScp914EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp914;

namespace CursedMod.Events.Handlers;

public static class CursedScp914EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerChangingScp914KnobSettingEventArgs> PlayerChangingScp914KnobSetting;
    
    public static event EventManager.CursedEventHandler<PlayerEnablingScp914EventArgs> PlayerEnablingScp914;
    
    public static event EventManager.CursedEventHandler<Scp914UpgradingItemEventArgs> UpgradingItem; 

    internal static void OnPlayerChangingScp914KnobSetting(PlayerChangingScp914KnobSettingEventArgs args)
    {
        PlayerChangingScp914KnobSetting.InvokeEvent(args);
    }
    
    internal static void OnPlayerEnablingScp914(PlayerEnablingScp914EventArgs args)
    {
        PlayerEnablingScp914.InvokeEvent(args);
    }
    
    internal static void OnUpgradingItem(Scp914UpgradingItemEventArgs args)
    {
        UpgradingItem.InvokeEvent(args);
    }
}
