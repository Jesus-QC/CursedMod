// -----------------------------------------------------------------------
// <copyright file="CursedFacilityEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CursedMod.Events.Arguments.Player;

namespace CursedMod.Events.Handlers;

public static class CursedFacilityEventsHandler
{
    public static event CursedEventManager.CursedEventHandler<RagdollSpawnedEventArgs> RagdollSpawned;
    
    internal static void OnRagdollSpawned(BasicRagdoll ragdoll)
    {
        RagdollSpawned.InvokeEvent(new RagdollSpawnedEventArgs(ragdoll));
    }
}