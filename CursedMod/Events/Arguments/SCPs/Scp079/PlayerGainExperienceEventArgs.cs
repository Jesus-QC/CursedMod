// -----------------------------------------------------------------------
// <copyright file="PlayerGainExperienceEventArgs.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using CursedMod.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079;

namespace CursedMod.Events.Arguments.SCPs.Scp079;

public class PlayerGainExperienceEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public PlayerGainExperienceEventArgs(Scp079TierManager tierManager, int experience, Scp079HudTranslation hudTranslation)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(tierManager.Owner);
        Experience = experience;
        HudTranslation = hudTranslation;
    }
    
    public bool IsAllowed { get; set; }
    
    public CursedPlayer Player { get; }
    
    public int Experience { get; set; }
    
    public Scp079HudTranslation HudTranslation { get; }
}