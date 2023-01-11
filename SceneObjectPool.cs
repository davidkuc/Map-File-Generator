using GhostOfTheLibrary.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GhostOfTheLibrary.LevelManager
{
    [Serializable]
    public class SceneObjectPool
    {
        public GameObject Prefab;

        public int PoolSize;
        public GameObject GroupGameObject;
        public List<GameObject> Pool;

        public SceneObjectPool() => Pool = new List<GameObject>();

        public void PopulatePool(List<GameObject> sceneObjectGroups)
        {
            Pool.Clear();

            for (int i = 0; i < PoolSize; i++)
            {
                var clonedSceneObject = Object.Instantiate(Prefab);
                clonedSceneObject.SetActive(false);
                Pool.Add(clonedSceneObject);
            }

            InsertSceneObjectsIntoGroup(sceneObjectGroups);
        }

        private void InsertSceneObjectsIntoGroup(List<GameObject> sceneObjectGroups)
        {
            var groupGameObject = new GameObject();
            this.GroupGameObject = groupGameObject;
            groupGameObject.SetActive(true);
            groupGameObject.name = $"{Pool[0].name.ClearStringFromParanthesesStart()} Pool";

            foreach (var item in Pool)
                item.transform.parent = groupGameObject.transform;

            sceneObjectGroups.Add(groupGameObject);
        }

        public GameObject GetSceneObject()
        {
            var sceneObject = Pool[0];
            sceneObject.SetActive(true);
            Pool.Remove(sceneObject);
            return sceneObject;
        }

        public void InsertIntoPool(GameObject go)
        {
            go.SetActive(false);
            Pool.Add(go);
        }
    }
}
