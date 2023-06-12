// -----------------------------------------------------------------------
// <copyright file="CursedScp0492EventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.SCPs.Scp0492;

namespace CursedMod.Events.Handlers;

public static class CursedScp0492EventsHandler
{
    public static event EventManager.CursedEventHandler<Scp0492ConsumingCorpseEventArgs> ConsumingCorpse;
    
    public static event EventManager.CursedEventHandler<Scp0492ConsumedCorpseEventArgs> ConsumedCorpse;

    internal static void OnConsumingCorpse(Scp0492ConsumingCorpseEventArgs args)
    {
        ConsumingCorpse.InvokeEvent(args);
    }
    
    internal static void OnConsumedCorpse(Scp0492ConsumedCorpseEventArgs args)
    {
        ConsumedCorpse.InvokeEvent(args);
    }
}