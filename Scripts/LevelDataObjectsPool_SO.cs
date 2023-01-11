using GhostOfTheLibrary.Books;
using GhostOfTheLibrary.Extensions;
using GhostOfTheLibrary.Other;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager
{
    [CreateAssetMenu(fileName = "LevelDataObjectsPool_SO_", menuName = "Scriptable Objects/Level Data Objects Pool")]
    public class LevelDataObjectsPool_SO : ScriptableObject, ICloneable
    {
        private const string LoadAllSceneObjectsPath = "Assets/Level Manager/Level Data Objects Pool/Load All Scene Objects";
        private const string SceneObjectsResourcesPath = "Level Manager/Scene Objects";

        public SerializableDictionary<string, SceneObjectPool> SceneObjectPoolsDictionary;
        public List<Book_SO> Books;
        public List<Client_SO> Clients;

        public void PopulatePools(List<GameObject> sceneObjectGroups)
        {
            foreach (var item in SceneObjectPoolsDictionary)
                item.Value.PopulatePool(sceneObjectGroups);
        }

        public void LoadSceneObjectsFromResources()
        {
            var selectedSO = (LevelDataObjectsPool_SO)Selection.activeObject;
            if (selectedSO.SceneObjectPoolsDictionary == null)
                selectedSO.SceneObjectPoolsDictionary = new SerializableDictionary<string, SceneObjectPool>();

            var sceneObjects = Resources.LoadAll<GameObject>(SceneObjectsResourcesPath);

            Debug.Log($"Found {sceneObjects.Length} scene objects");

            foreach (var item in sceneObjects)
                selectedSO.SceneObjectPoolsDictionary.Add(item.name, new SceneObjectPool() { Prefab = item });
        }

        [MenuItem(LoadAllSceneObjectsPath, false, 2000)]
        static void LoadSceneObjectsFromResourcesStatic()
        {
            var selectedSO = (LevelDataObjectsPool_SO)Selection.activeObject;
            if (selectedSO.SceneObjectPoolsDictionary == null)
                selectedSO.SceneObjectPoolsDictionary = new SerializableDictionary<string, SceneObjectPool>();

            var sceneObjects = Resources.LoadAll<GameObject>(SceneObjectsResourcesPath);

            Debug.Log($"Found {sceneObjects.Length} scene objects");

            foreach (var item in sceneObjects)
                selectedSO.SceneObjectPoolsDictionary.Add(item.name, new SceneObjectPool() { Prefab = item });
        }

        [MenuItem(LoadAllSceneObjectsPath, true)]
        static bool ValidateType() => Selection.activeObject.GetType() == typeof(LevelDataObjectsPool_SO);

        public object Clone()
        {
            var clonedObject = Instantiate(this);

            clonedObject.SceneObjectPoolsDictionary = (SerializableDictionary<string, SceneObjectPool>)clonedObject.SceneObjectPoolsDictionary?.Clone();
            clonedObject.Books = (List<Book_SO>)Books?.CloneScriptableObjectList();
            clonedObject.Clients = (List<Client_SO>)Clients?.CloneScriptableObjectList();

            return clonedObject;
        }
    }
}
