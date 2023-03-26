// -----------------------------------------------------------------------
// <copyright file="CursedPrimitiveObject.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AdminToys;
using UnityEngine;

namespace CursedMod.Features.Wrappers.AdminToys;

public class CursedPrimitiveObject : CursedAdminToy
{
    internal CursedPrimitiveObject(PrimitiveObjectToy primitive)
        : base(primitive)
    {
        Base = primitive;
    }

    public PrimitiveObjectToy Base { get; }

    public PrimitiveType PrimitiveType
    {
        get => Base.PrimitiveType;
        set => Base.NetworkPrimitiveType = value;
    }
    
    public Color Color
    {
        get => Base.MaterialColor;
        set => Base.NetworkMaterialColor = value;
    }
    
    public static CursedPrimitiveObject Create(PrimitiveType? type = null, Vector3? position = null, Vector3? scale = null, Vector3? rotation = null, Color? color = null)
    {
        PrimitiveObjectToy primitiveObjectToy = Object.Instantiate(CursedPrefabManager.PrimitiveObject);
        CursedPrimitiveObject primitive = new (primitiveObjectToy);

        if (type.HasValue)
            primitive.PrimitiveType = type.Value;
        
        if (position.HasValue)
            primitive.Position = position.Value;

        if (scale.HasValue)
            primitive.Scale = scale.Value;

        if (rotation.HasValue)
            primitive.Rotation = rotation.Value;

        if (color.HasValue)
            primitive.Color = color.Value;

        return primitive;
    }
}