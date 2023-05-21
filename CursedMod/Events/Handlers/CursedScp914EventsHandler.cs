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
    public static event EventManager.CursedEventHandler<PlayerChangeKnobSettingEventArgs> PlayerChangeKnobSetting;
    
    public static event EventManager.CursedEventHandler<PlayerStart914EventArgs> PlayerStart;
    
    public static event EventManager.CursedEventHandler<PlayerUpgradeItemEventArgs> PlayerUpgradeItem; 

    internal static void OnPlayerChangeKnobSetting(PlayerChangeKnobSettingEventArgs args)
    {
        PlayerChangeKnobSetting.InvokeEvent(args);
    }
    
    internal static void OnPlayerStart914(PlayerStart914EventArgs args)
    {
        PlayerStart.InvokeEvent(args);
    }
    
    internal static void OnPlayerUpgradeItem(PlayerUpgradeItemEventArgs args)
    {
        PlayerUpgradeItem.InvokeEvent(args);
    }
}