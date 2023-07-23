// -----------------------------------------------------------------------
// <copyright file="CursedFirearmItem.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using CursedMod.Features.Wrappers.Player;
using Footprinting;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using InventorySystem.Items.Firearms.BasicMessages;
using InventorySystem.Items.Firearms.Modules;

namespace CursedMod.Features.Wrappers.Inventory.Items.Firearms;

public class CursedFirearmItem : CursedItem
{
    internal CursedFirearmItem(Firearm itemBase)
        : base(itemBase)
    {
        FirearmBase = itemBase;
    }
    
    public Firearm FirearmBase { get; }

    public FirearmBaseStats BaseStats => FirearmBase.BaseStats;

    public float ArmorPenetration => FirearmBase.ArmorPenetration;

    public IAmmoManagerModule AmmoManagerModule
    {
        get => FirearmBase.AmmoManagerModule;
        set => FirearmBase.AmmoManagerModule = value;
    }

    public byte Ammo
    {
        get => Status.Ammo;
        set => Status = new FirearmStatus(value, Status.Flags, Status.Attachments);
    }
    
    public byte MaxAmmo => AmmoManagerModule.MaxAmmo;

    public IEquipperModule EquipperModule
    {
        get => FirearmBase.EquipperModule;
        set => FirearmBase.EquipperModule = value;
    }

    public IActionModule ActionModule
    {
        get => FirearmBase.ActionModule;
        set => FirearmBase.ActionModule = value;
    }

    public IInspectorModule InspectorModule
    {
        get => FirearmBase.InspectorModule;
        set => FirearmBase.InspectorModule = value;
    }

    public IHitregModule HitregModule
    {
        get => FirearmBase.HitregModule;
        set => FirearmBase.HitregModule = value;
    }

    public IAdsModule AdsModule
    {
        get => FirearmBase.AdsModule;
        set => FirearmBase.AdsModule = value;
    }

    public FirearmStatus Status
    {
        get => FirearmBase.Status;
        set => FirearmBase.Status = value;
    }

    public Footprint Footprint => FirearmBase.Footprint;

    public bool AllowDisarming => FirearmBase.AllowDisarming;
    
    public float StaminaUsageMultiplier => FirearmBase.StaminaUsageMultiplier;
    
    public float MovementSpeedMultiplier => FirearmBase.MovementSpeedMultiplier;
    
    public float StaminaRegenMultiplier => FirearmBase.StaminaRegenMultiplier;
    
    public float MovementSpeedLimit => FirearmBase.MovementSpeedLimit;
    
    public bool SprintingDisabled => FirearmBase.SprintingDisabled;

    public float Lenght => FirearmBase.Length;

    public bool IsEmittingLight => FirearmBase.IsEmittingLight;

    public bool MovementModifierActive => FirearmBase.MovementModifierActive;

    public bool StaminaModifierActive => FirearmBase.StaminaModifierActive;

    public bool IsAiming => AdsModule.ServerAds;
    
    public bool FlashlightEnabled
    {
        get => Status.Flags.HasFlagFast(FirearmStatusFlags.FlashlightEnabled);
        set
        {
            FirearmStatusFlags flags = Status.Flags;
            if (value)
                flags = flags | FirearmStatusFlags.FlashlightEnabled;
            else
                flags = flags & ~FirearmStatusFlags.FlashlightEnabled;

            Status = new FirearmStatus(Status.Ammo, flags, Status.Attachments);
        }
    }

    public float BaseWeight
    {
        get => FirearmBase.BaseWeight;
        set => FirearmBase.BaseWeight = value;
    }

    public float BaseLenght
    {
        get => FirearmBase.BaseLength;
        set => FirearmBase.BaseLength = value;
    }

    public Attachment[] Attachments
    {
        get => FirearmBase.Attachments;
        set => FirearmBase.Attachments = value;
    }
    
    public uint AttachmentsCode
    {
        get => Status.Attachments;
        set => Status = new FirearmStatus(Status.Ammo, Status.Flags, value);
    }
    
    public static CursedFirearmItem Get(Firearm firearm)
    {
        if (firearm is AutomaticFirearm automaticFirearm)
            return new CursedAutomaticFirearmItem(automaticFirearm);

        return new CursedFirearmItem(firearm);
    }

    public IEnumerable<CursedFirearmAttachment> GetAttachments() => Attachments.Select(CursedFirearmAttachment.Get);

    public void SetPlayerAttachments(CursedPlayer player)
    {
        if (player is null)
            return;

        if (AttachmentsServerHandler.PlayerPreferences.TryGetValue(player.ReferenceHub, out Dictionary<ItemType, uint> value) && value.TryGetValue(FirearmBase.ItemTypeId, out uint value2))
            FirearmBase.ApplyAttachmentsCode(value2, reValidate: true);

        FirearmStatusFlags firearmStatusFlags = FirearmStatusFlags.MagazineInserted;
        if (FirearmBase.HasAdvantageFlag(AttachmentDescriptiveAdvantages.Flashlight))
            firearmStatusFlags |= FirearmStatusFlags.FlashlightEnabled;

        FirearmBase.Status = new FirearmStatus(FirearmBase.AmmoManagerModule.MaxAmmo, firearmStatusFlags, FirearmBase.GetCurrentAttachmentsCode());
    }
}
