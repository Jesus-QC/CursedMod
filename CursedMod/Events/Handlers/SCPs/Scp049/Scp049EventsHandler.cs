// -----------------------------------------------------------------------
// <copyright file="Scp049EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp049;

namespace CursedMod.Events.Handlers.SCPs.Scp049;

public static class Scp049EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerStartReviveEventArgs> PlayerStartRevive;
    
    public static event EventManager.CursedEventHandler<PlayerFinishReviveEventArgs> PlayerFinishRevive;
    
    public static event EventManager.CursedEventHandler<PlayerSensingEventArgs> PlayerSensing;
    
    public static event EventManager.CursedEventHandler<PlayerCallingEventArgs> PlayerCalling; 

    internal static void OnPlayerReviving(PlayerStartReviveEventArgs args)
    {
        PlayerStartRevive.InvokeEvent(args);
    }
    
    internal static void OnPlayerRevived(PlayerFinishReviveEventArgs args)
    {
        PlayerFinishRevive.InvokeEvent(args);
    }
    
    internal static void OnPlayerSensing(PlayerSensingEventArgs args)
    {
        PlayerSensing.InvokeEvent(args);
    }
    
    internal static void OnPlayerCalling(PlayerCallingEventArgs args)
    {
        PlayerCalling.InvokeEvent(args);
    }
}