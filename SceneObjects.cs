using GhostOfTheLibrary.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager
{
    [Serializable]
    public class SceneObjects : ICloneable
    {
        // Ładowane w trakcie pobierania obiektów z LevelDataObjectsPool_SO
        public List<GameObject> Objects;

        // Ładowane w trakcie zapisywania pozycji obiektow w scenie
        public List<SceneObjectTransform> Transforms;

        public int SceneObjectCount => Transforms.Count;

        public SceneObjects()
        {
            Objects = new List<GameObject>();
            Transforms = new List<SceneObjectTransform>();
        }

        public void AddSceneObject(GameObject sceneObject, SceneObjectTransform transformValues)
        {
            sceneObject.transform.position = transformValues.Position;
            sceneObject.transform.rotation = transformValues.Rotation;
            sceneObject.transform.localScale = transformValues.Scale;

            Objects.Add(sceneObject);
        }

        public GameObject RemoveSceneObjectFromMap()
        {
            var sceneObject = Objects[0];
            Objects.Remove(sceneObject);
            return sceneObject;
        }

        public void ClearSceneObjects() => Objects.Clear();

        public object Clone()
        {
            var clonedObject = new SceneObjects();
            clonedObject.Objects = (List<GameObject>)Objects.CloneGameObjectList();
            clonedObject.Transforms = new List<SceneObjectTransform>(Transforms);

            return clonedObject;
        }
    }
}
