// -----------------------------------------------------------------------
// <copyright file="CursedFirearmAttachment.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;

namespace CursedMod.Features.Wrappers.Inventory.Items.Firearms;

public class CursedFirearmAttachment
{
    private CursedFirearmAttachment(Attachment attachment)
    {
        Base = attachment;
    }
    
    public Attachment Base { get; }

    public AttachmentName AttachmentName => Base.Name;

    public AttachmentSlot Slot => Base.Slot;

    public float Weight => Base.Weight;

    public float Lenght => Base.Length;

    public AttachmentDescriptiveAdvantages AttachmentDescriptiveAdvantages => Base.DescriptivePros;
    
    public AttachmentDescriptiveDownsides AttachmentDescriptiveDownsides => Base.DescriptiveCons;

    public bool IsEnabled
    {
        get => Base.IsEnabled;
        set => Base.IsEnabled = value;
    }

    public int AttachmentId
    {
        get => Base.AttachmentId;
        set => Base.AttachmentId = value;
    }
    
    public static CursedFirearmAttachment Get(Attachment attachment) => new (attachment);
    
    public void SetParameterValue(AttachmentParameterValuePair pair) => Base.SetParameterValue(pair);

    public void SetParameterValue(AttachmentParam param, float val) => Base.SetParameterValue(param, val);

    public void SetParameterValue(int param, float val) => Base.SetParameterValue(param, val);

    public void ResetParameter(AttachmentParam param) => Base.ResetParameter(param);

    public bool TryGetValue(int param, out float val) => Base.TryGetValue(param, out val);
    
    public bool TryGetValue(AttachmentParam param, out float val) => Base.TryGetValue(param, out val);

    public bool TryGetParentFirearm(out Firearm firearm) => Base.TryGetParentFirearm(out firearm);

    public void GetNameAndDescription(out string name, out string description)
        => Base.GetNameAndDescription(out name, out description);

    public void GetActiveParamsNonAlloc(AttachmentParam[] activeParams, out int len)
        => Base.GetActiveParamsNonAlloc(activeParams, out len);
}