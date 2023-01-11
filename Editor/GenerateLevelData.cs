using GhostOfTheLibrary.Extensions;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager.Editor
{
    public class GenerateLevelData : EditorWindow
    {
        private const string LevelDataList_SO_Path = "Assets/Resources/Level Manager/LevelDataList_SO.asset";
        private const string GenerateLevelDataPath = "GameObject/Level Manager/Generate Level Data";

        private static readonly Vector2Int size = new Vector2Int(250, 100);
        private string levelName;
        private GameObject selectedLevelGameObject;

        [MenuItem(GenerateLevelDataPath, priority = -2000)]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<GenerateLevelData>();
            window.minSize = size;
            window.maxSize = size;
        }

        private void OnGUI()
        {
            levelName = EditorGUILayout.TextField("Level Name", levelName);
            if (GUILayout.Button("Generate Level Data"))
            {
                LevelDataList_SO levelDataList = AssetDatabase.LoadAssetAtPath<LevelDataList_SO>(LevelDataList_SO_Path);
                GameObject[] selectedLevelGameObjectArray = new GameObject[1];

                if (selectedLevelGameObject == null)
                {
                    selectedLevelGameObjectArray = Selection.gameObjects;

                    if (selectedLevelGameObjectArray == null || selectedLevelGameObjectArray.Length == 0)
                    {
                        Debug.LogError($"No level gameobject selected in the hierarchy!");
                        return;
                    }
                }

                selectedLevelGameObject = selectedLevelGameObjectArray[0];

                Debug.Log($"Generating map from {selectedLevelGameObject.name} gameobject in hierarchy");

                LevelData_SO level_SO = ScriptableObject.CreateInstance<LevelData_SO>();
                string path = $"Assets/Resources/Level Manager/Levels Data/{levelName}.asset";
                AssetDatabase.CreateAsset(level_SO, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();

                if (level_SO.SceneObjectsDictionary == null)
                    level_SO.SceneObjectsDictionary = new SerializableDictionary<string, SceneObjects>();

                List<GameObject> allGameObjects = GetSceneObjects(selectedLevelGameObject);
                foreach (var gameObject in allGameObjects)
                {
                    string formattedGameObjectName = gameObject.name.ClearStringFromParanthesesStart();
                    if (level_SO.SceneObjectsDictionary.ContainsKey(formattedGameObjectName))
                        AddTransformToSceneObject(level_SO, gameObject, formattedGameObjectName);
                    else
                    {
                        level_SO.SceneObjectsDictionary.Add(formattedGameObjectName, new SceneObjects());
                        AddTransformToSceneObject(level_SO, gameObject, formattedGameObjectName);
                    }

                    Debug.Log($"Added {formattedGameObjectName}");
                }

                EditorUtility.SetDirty(level_SO);
                Selection.activeObject = level_SO;

                if (levelDataList == null)
                {
                    Debug.LogWarning($"Level Data List scriptable object not found in path: {LevelDataList_SO_Path}" +
                        $"\r\n You need to reload Levels Data manually " +
                        $"--> right click on LevelDataList_SO " +
                        $"--> Level Manager " +
                        $"-->  Level Data List" +
                        $" --> Load All Levels Data");

                    return;
                }

                levelDataList.LoadLevelsFromResources();
                Debug.Log("Level Data List scriptable object loaded all levels data.");
            }
        }

        private void AddTransformToSceneObject(LevelData_SO level_SO, GameObject gameObject, string formattedGameObjectName)
        {
            level_SO.SceneObjectsDictionary[formattedGameObjectName]
           .Transforms.Add(new SceneObjectTransform()
           {
               Position = gameObject.transform.position,
               Rotation = gameObject.transform.rotation,
               Scale = gameObject.transform.localScale
           });
        }

        private List<GameObject> GetSceneObjects(GameObject levelGameObject)
        {
            var gameObjects = new List<GameObject>();
            var transforms = levelGameObject.transform.GetComponentsInChildren<Transform>();

            foreach (var item in transforms)
            {
                if (item.CompareTag("sceneObject"))
                    gameObjects.Add(item.gameObject);
            }

            return gameObjects;
        }
    }
}
