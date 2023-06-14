// -----------------------------------------------------------------------
// <copyright file="CursedScp939EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp939;

namespace CursedMod.Events.Handlers;

public static class CursedScp939EventsHandler
{
    public static event CursedEventManager.CursedEventHandler<Scp939SavingVoiceEventArgs> SavingVoice;
    
    public static event CursedEventManager.CursedEventHandler<Scp939SavedVoiceEventArgs> SavedVoice;
    
    public static event CursedEventManager.CursedEventHandler<Scp939PlacingAmnesticCloudEventArgs> PlacingAmnesticCloud; 
    
    public static event CursedEventManager.CursedEventHandler<Scp939CancellingCloudPlacementEventArgs> CancellingCloudPlacement;
    
    public static event CursedEventManager.CursedEventHandler<Scp939PlayingSoundEventArgs> PlayingSound;
    
    public static event CursedEventManager.CursedEventHandler<Scp939PlayingVoiceEventArgs> PlayingVoice;
    
    public static event CursedEventManager.CursedEventHandler<Scp939UsingLungeAbilityEventArgs> UsingLungeAbility;
    
    public static event CursedEventManager.CursedEventHandler<Scp939PlacingMimicPointEventArgs> PlacingMimicPoint;
    
    public static event CursedEventManager.CursedEventHandler<Scp939RemovingMimicPointEventArgs> RemovingMimic;
    
    public static event CursedEventManager.CursedEventHandler<Scp939RemovingSavedVoiceEventArgs> RemovingSavedVoice;
    
    public static event CursedEventManager.CursedEventHandler<Scp939UsingFocusAbilityEventArgs> UsingFocusAbility; 

    internal static void OnSavingVoice(Scp939SavingVoiceEventArgs args)
    {
        SavingVoice.InvokeEvent(args);
    }

    internal static void OnSavedVoice(Scp939SavedVoiceEventArgs args)
    {
        SavedVoice.InvokeEvent(args);
    }
    
    internal static void OnPlacingAmnesticCloud(Scp939PlacingAmnesticCloudEventArgs args)
    {
        PlacingAmnesticCloud.InvokeEvent(args);
    }
    
    internal static void OnCancellingCloudPlacement(Scp939CancellingCloudPlacementEventArgs args)
    {
        CancellingCloudPlacement.InvokeEvent(args);
    }
    
    internal static void OnPlayingSound(Scp939PlayingSoundEventArgs args)
    {
        PlayingSound.InvokeEvent(args);
    }
    
    internal static void OnPlayingVoice(Scp939PlayingVoiceEventArgs args)
    {
        PlayingVoice.InvokeEvent(args);
    }
    
    internal static void OnUsingLungeAbility(Scp939UsingLungeAbilityEventArgs args)
    {
        UsingLungeAbility.InvokeEvent(args);
    }
    
    internal static void OnPlacingMimicPoint(Scp939PlacingMimicPointEventArgs args)
    {
        PlacingMimicPoint.InvokeEvent(args);
    }
    
    internal static void OnRemovingMimicPoint(Scp939RemovingMimicPointEventArgs args)
    {
        RemovingMimic.InvokeEvent(args);
    }
    
    internal static void OnRemovingSavedVoice(Scp939RemovingSavedVoiceEventArgs args)
    {
        RemovingSavedVoice.InvokeEvent(args);
    }
    
    internal static void OnUsingFocusAbility(Scp939UsingFocusAbilityEventArgs args)
    {
        UsingFocusAbility.InvokeEvent(args);
    }
}