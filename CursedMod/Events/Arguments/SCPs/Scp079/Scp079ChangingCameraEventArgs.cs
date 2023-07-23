// -----------------------------------------------------------------------
// <copyright file="Scp079ChangingCameraEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Facility.Rooms;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class Scp079ChangingCameraEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public Scp079ChangingCameraEventArgs(Scp079CurrentCameraSync currentCameraSync)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(currentCameraSync.Owner);
        Camera = Cursed079Camera.Get(currentCameraSync.CurrentCamera);
    }
    
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; }
    
    public Cursed079Camera Camera { get; }
}