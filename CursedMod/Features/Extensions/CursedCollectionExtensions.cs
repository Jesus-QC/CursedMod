// -----------------------------------------------------------------------
// <copyright file="CursedCollectionExtensions.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace CursedMod.Features.Extensions;

public static class CursedCollectionExtensions
{
    public static T GetRandomElement<T>(this List<T> collection)
    {
        return collection[Random.Range(0, collection.Count)];
    }
    
    public static void SetOrAddElement<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
            return;
        }

        dictionary[key] = value;
    }
}