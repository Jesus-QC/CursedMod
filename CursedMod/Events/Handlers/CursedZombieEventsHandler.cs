// -----------------------------------------------------------------------
// <copyright file="CursedZombieEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp0492;

namespace CursedMod.Events.Handlers;

public static class CursedZombieEventsHandler
{
    public static event EventManager.CursedEventHandler<PlayerConsumingCorpseEventArgs> PlayerConsumingCorpse;
    
    public static event EventManager.CursedEventHandler<PlayerCorpseConsumedEventArgs> PlayerCorpseConsumed;
    
    public static event EventManager.CursedEventHandler<PlayerBloodlustingEventArgs> PlayerBloodlusting; 

    internal static void OnPlayerConsumingCorpse(PlayerConsumingCorpseEventArgs args)
    {
        PlayerConsumingCorpse.InvokeEvent(args);
    }
    
    internal static void OnPlayerCorpseConsumed(PlayerCorpseConsumedEventArgs args)
    {
        PlayerCorpseConsumed.InvokeEvent(args);
    }
    
    internal static void OnPlayerBloodlusting(PlayerBloodlustingEventArgs args)
    {
        PlayerBloodlusting.InvokeEvent(args);
    }
}