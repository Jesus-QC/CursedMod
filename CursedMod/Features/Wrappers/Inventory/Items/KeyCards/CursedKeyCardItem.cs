// -----------------------------------------------------------------------
// <copyright file="CursedKeyCardItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;

namespace CursedMod.Features.Wrappers.Inventory.Items.KeyCards;

public class CursedKeyCardItem : CursedItem
{
    internal CursedKeyCardItem(KeycardItem itemBase) 
        : base(itemBase)
    {
        KeyCardBase = itemBase;
    }
    
    public KeycardItem KeyCardBase { get; }

    public KeycardPermissions Permissions
    {
        get => KeyCardBase.Permissions;
        set => KeyCardBase.Permissions = value;
    }
}