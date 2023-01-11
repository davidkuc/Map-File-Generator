using GhostOfTheLibrary.Books;
using GhostOfTheLibrary.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager
{
    [CreateAssetMenu(fileName = "LevelData_SO_", menuName = "Scriptable Objects/Level Data")]
    public class LevelData_SO : ScriptableObject, ICloneable
    {
        // Tutaj ladowane sa obiekty (sklonowane prefaby) z LevelDataObjectsPool_SO, potem ich Transformy sa zmieniane na wartosci obecne w LevelDataObjects.Tranforms
        public SerializableDictionary<string, SceneObjects> SceneObjectsDictionary;
        public List<Book_SO> Books;
        public List<Client_SO> Clients;

        public void LoadLevel(LevelDataObjectsPool_SO levelDataObjectsPool)
        {
            foreach (var item in SceneObjectsDictionary)
            {
                item.Value.ClearSceneObjects();

                for (int i = 0; i < item.Value.SceneObjectCount; i++)
                {
                    var sceneObject = levelDataObjectsPool.SceneObjectPoolsDictionary[item.Key].GetSceneObject();
                    item.Value.AddSceneObject(sceneObject, item.Value.Transforms[i]);
                }
            }
        }

        public void UnloadLevel(LevelDataObjectsPool_SO levelDataObjectsPool)
        {
            foreach (var item in SceneObjectsDictionary)
            {
                while (item.Value.Objects.Count > 0)
                {
                    var sceneObjectToUnload = item.Value.RemoveSceneObjectFromMap();
                    levelDataObjectsPool.SceneObjectPoolsDictionary[item.Key].InsertIntoPool(sceneObjectToUnload);
                }
            }
        }

        public object Clone()
        {
            var clonedObject = Instantiate(this);

            clonedObject.SceneObjectsDictionary = (SerializableDictionary<string, SceneObjects>)SceneObjectsDictionary?.Clone();
            clonedObject.Books = (List<Book_SO>)Books?.CloneScriptableObjectList();
            clonedObject.Clients = (List<Client_SO>)Clients?.CloneScriptableObjectList();

            return clonedObject;
        }
    }
}
