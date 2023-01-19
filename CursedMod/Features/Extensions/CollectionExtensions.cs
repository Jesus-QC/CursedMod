using System.Collections.Generic;
using UnityEngine;

namespace CursedMod.Features.Extensions;

public static class CollectionExtensions
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