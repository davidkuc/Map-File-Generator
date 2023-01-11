using System.Collections.Generic;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] LevelDataObjectsPool_SO levelDataObjectsPool;
        [SerializeField] LevelDataList_SO levelDataList;
        [SerializeField] List<GameObject> sceneObjectGroups;

        [Header("Debugging")]
        [SerializeField] int levelIndex;

        bool isLevelLoaded;
        int loadedlevelIndex;

        private void Awake()
        {
            if (levelDataObjectsPool.SceneObjectPoolsDictionary.Count == 0)
                levelDataObjectsPool.LoadSceneObjectsFromResources();

            levelDataObjectsPool.PopulatePools(sceneObjectGroups);

            if (levelDataList.LevelDataList.Count == 0)
                levelDataList.LoadLevelsFromResources();
        }

        [ContextMenu("Test Unload Level")]
        public void UnloadLevel()
        {
            if (loadedlevelIndex != levelIndex)
            {
                Debug.Log("Your trying to unload a different map that isnt loaded!");
                return;
            }

            levelDataList.LevelDataList[levelIndex].UnloadLevel(levelDataObjectsPool);
            isLevelLoaded = false;
            loadedlevelIndex = -1;
        }

        [ContextMenu("Test Load Level")]
        public void LoadLevel()
        {
            if (isLevelLoaded)
            {
                Debug.Log("Level is already loaded!");
                return;
            }

            levelDataList.LevelDataList[levelIndex].LoadLevel(levelDataObjectsPool);
            isLevelLoaded = true;
            loadedlevelIndex = levelIndex;
        }
    }
}
