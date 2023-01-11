using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GhostOfTheLibrary.Extensions
{
    public static class CloningExtensions
    {
        public static object CloneGameObjectList<T>(this List<T> list) where T : Object
        {
            var clonedObject = new List<T>();

            foreach (var item in list)
                clonedObject.Add(Object.Instantiate(item));

            return clonedObject;
        }

        public static object CloneScriptableObjectList<T>(this List<T> list) where T : ScriptableObject, ICloneable
        {
            var clonedObject = new List<T>();

            foreach (var item in list)
                clonedObject.Add((T)item.Clone());

            return clonedObject;
        }
    }
}
