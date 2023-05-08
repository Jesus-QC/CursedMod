// -----------------------------------------------------------------------
// <copyright file="Scp939EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp939;

namespace CursedMod.Events.Handlers.SCPs.Scp939;

public static class Scp939EventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerSaveVoiceEventArgs> PlayerSaveVoice;
    
    public static event EventManager.CursedEventHandler<PlayerPlaceAmnesticCloudEventArgs> PlayerPlaceAmnesticCloud; 
    
    public static event EventManager.CursedEventHandler<PlayerCancelCloudPlacementEventArgs> PlayerCancelCloudPlacement;
    
    public static event EventManager.CursedEventHandler<PlayerPlaySoundEventArgs> PlayerPlaySound;
    
    public static event EventManager.CursedEventHandler<PlayerPlayVoiceEventArgs> PlayerPlayVoice;
    
    public static event EventManager.CursedEventHandler<PlayerLungeEventArgs> PlayerLunge;
    
    public static event EventManager.CursedEventHandler<PlayerPlaceMimicPointEventArgs> PlayerPlaceMimicPoint;
    
    public static event EventManager.CursedEventHandler<PlayerRemoveMimicPointEventArgs> PlayerRemoveMimicPoint;
    
    public static event EventManager.CursedEventHandler<PlayerRemoveSavedVoiceEventArgs> PlayerRemoveSavedVoice;
    
    public static event EventManager.CursedEventHandler<PlayerUseFocusEventArgs> PlayerUseFocus; 

    internal static void OnPlayerSaveVoice(PlayerSaveVoiceEventArgs args)
    {
        PlayerSaveVoice.InvokeEvent(args);
    }
    
    internal static void OnPlayerPlaceAmnesticCloud(PlayerPlaceAmnesticCloudEventArgs args)
    {
        PlayerPlaceAmnesticCloud.InvokeEvent(args);
    }
    
    internal static void OnPlayerCancelCloudPlacement(PlayerCancelCloudPlacementEventArgs args)
    {
        PlayerCancelCloudPlacement.InvokeEvent(args);
    }
    
    internal static void OnPlayerPlaySound(PlayerPlaySoundEventArgs args)
    {
        PlayerPlaySound.InvokeEvent(args);
    }
    
    internal static void OnPlayerPlayVoice(PlayerPlayVoiceEventArgs args)
    {
        PlayerPlayVoice.InvokeEvent(args);
    }
    
    internal static void OnPlayerLunge(PlayerLungeEventArgs args)
    {
        PlayerLunge.InvokeEvent(args);
    }
    
    internal static void OnPlayerPlaceMimicPoint(PlayerPlaceMimicPointEventArgs args)
    {
        PlayerPlaceMimicPoint.InvokeEvent(args);
    }
    
    internal static void OnPlayerRemoveMimicPoint(PlayerRemoveMimicPointEventArgs args)
    {
        PlayerRemoveMimicPoint.InvokeEvent(args);
    }
    
    internal static void OnPlayerRemoveSavedVoice(PlayerRemoveSavedVoiceEventArgs args)
    {
        PlayerRemoveSavedVoice.InvokeEvent(args);
    }
    
    internal static void OnPlayerUseFocus(PlayerUseFocusEventArgs args)
    {
        PlayerUseFocus.InvokeEvent(args);
    }
}