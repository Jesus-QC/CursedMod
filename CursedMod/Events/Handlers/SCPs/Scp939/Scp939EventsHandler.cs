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
    public static event EventManager.CursedEventHandler<PlayerSavingVoiceEventArgs> PlayerSavingVoice;
    
    public static void OnPlayerSavingVoice(PlayerSavingVoiceEventArgs args)
    {
        PlayerSavingVoice.InvokeEvent(args);
    }
}