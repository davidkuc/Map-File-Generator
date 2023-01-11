using System;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager
{
    [Serializable]
    public class SceneObjectTransform : ICloneable
    {
        public Vector3 Position;

        public Vector3 Scale;

        public Quaternion Rotation;

        public object Clone()
        {
            var clonedObject = new SceneObjectTransform();
            clonedObject.Position = Position;
            clonedObject.Scale = Scale;
            clonedObject.Rotation = Rotation;

            return clonedObject;
        }
    }
}
