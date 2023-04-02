// -----------------------------------------------------------------------
// <copyright file="DynamicEventPatchAttribute.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

namespace CursedMod.Events;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DynamicEventPatchAttribute : Attribute
{
    public DynamicEventPatchAttribute(Type eventHandlerType, string eventName)
    {
        EventInfo = eventHandlerType.GetField(eventName, BindingFlags.NonPublic | BindingFlags.Static);
    }
    
    public FieldInfo EventInfo { get; }
}