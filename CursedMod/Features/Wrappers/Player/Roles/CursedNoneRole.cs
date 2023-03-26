// -----------------------------------------------------------------------
// <copyright file="CursedNoneRole.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles;
using PlayerRoles.Voice;

namespace CursedMod.Features.Wrappers.Player.Roles;

public class CursedNoneRole : CursedRole
{
    internal CursedNoneRole(NoneRole roleBase)
        : base(roleBase)
    {
        NoneRoleBase = roleBase;
    }
    
    public NoneRole NoneRoleBase { get; }

    public VoiceModuleBase VoiceModule
    {
        get => NoneRoleBase.VoiceModule;
        set => NoneRoleBase.VoiceModule = value;
    }
}