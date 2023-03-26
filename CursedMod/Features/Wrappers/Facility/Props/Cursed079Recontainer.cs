// -----------------------------------------------------------------------
// <copyright file="Cursed079Recontainer.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Interactables.Interobjects.DoorUtils;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Props;

/// <summary>
/// Represents the Button that contains the Scp-079
/// </summary>
public static class Cursed079Recontainer
{
    public static Scp079Recontainer RecontainerBase { get; internal set; }

    public static HashSet<DoorVariant> LockedDoors => RecontainerBase._lockedDoors;

    public static bool CassieBusy => RecontainerBase.CassieBusy;

    public static float OverchargeDelay
    {
        get => RecontainerBase._activationDelay;
        set => RecontainerBase._activationDelay = value;
    }
    
    public static float LockdownDuration
    {
        get => RecontainerBase._lockdownDuration;
        set => RecontainerBase._lockdownDuration = value;
    }
    
    public static Vector3 ButtonPosition => RecontainerBase._activatorPos;
    
    public static float ActivatorLerpSpeed
    {
        get => RecontainerBase._activatorLerpSpeed;
        set => RecontainerBase._activatorLerpSpeed = value;
    }
    
    public static string AnnouncementProgress
    {
        get => RecontainerBase._announcementProgress;
        set => RecontainerBase._announcementProgress = value;
    }
    
    public static string AnnouncementCountdown
    {
        get => RecontainerBase._announcementCountdown;
        set => RecontainerBase._announcementCountdown = value;
    }
    
    public static string SuccessAnnouncement
    {
        get => RecontainerBase._announcementSuccess;
        set => RecontainerBase._announcementSuccess = value;
    }
    
    public static bool IsContainmentSequenceDone
    {
        get => RecontainerBase._alreadyRecontained;
        set => RecontainerBase._alreadyRecontained = value;
    }

    public static void SetContainmentDoors(bool opened, bool locked) => RecontainerBase.SetContainmentDoors(opened, locked);
    
    public static bool IsScpButNo079(PlayerRoleBase playerRoleBase) => RecontainerBase.IsScpButNot079(playerRoleBase);
    
    public static bool TryKillScp079() => RecontainerBase.TryKill079();
    
    public static void PlayAnnouncement(string announcement, float glitchMultiplier) => RecontainerBase.PlayAnnouncement(announcement, glitchMultiplier);
    
    public static void StartOvercharge() => RecontainerBase.BeginOvercharge();
    
    public static void FinishOvercharge() => RecontainerBase.EndOvercharge();
    
    public static void RefreshEngagementStatus() => RecontainerBase.RefreshAmount();
    
    public static void Recontain() => RecontainerBase.Recontain();
}