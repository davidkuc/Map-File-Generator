using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GhostOfTheLibrary.LevelManager
{
    [CreateAssetMenu(fileName = "LevelDataList_SO_", menuName = "Scriptable Objects/Level Data List")]
    public class LevelDataList_SO : ScriptableObject, ICloneable
    {
        private const string LoadAllLevelsDataPath = "Assets/Level Manager/Level Data List/Load All Levels Data";
        private const string LevelsDataResourcesPath = "Level Manager/Levels Data";

        public List<LevelData_SO> LevelDataList;

        public void LoadLevelsFromResources()
        {
            if (LevelDataList == null)
                LevelDataList = new List<LevelData_SO>();

            LevelDataList.Clear();

            var levels = Resources.LoadAll<LevelData_SO>(LevelsDataResourcesPath);

            Debug.Log($"Found {levels.Length} levels");

            foreach (var item in levels)
                LevelDataList.Add(item);
        }

        [MenuItem(LoadAllLevelsDataPath, false, 2000)]
        static void LoadLevelsFromResourcesStatic()
        {
            var selectedSO = (LevelDataList_SO)Selection.activeObject;

            if (selectedSO.LevelDataList == null)
                selectedSO.LevelDataList = new List<LevelData_SO>();

            selectedSO.LevelDataList.Clear();

            var levels = Resources.LoadAll<LevelData_SO>(LevelsDataResourcesPath);

            Debug.Log($"Found {levels.Length} levels");

            foreach (var item in levels)
                selectedSO.LevelDataList.Add(item);
        }

        [MenuItem(LoadAllLevelsDataPath, true)]
        static bool ValidateType() => Selection.activeObject.GetType() == typeof(LevelDataList_SO);

        public object Clone()
        {
            var clonedObject = Instantiate(this);
            List<LevelData_SO> clonedLevelDataList = new List<LevelData_SO>();

            foreach (var item in LevelDataList)
                clonedLevelDataList.Add((LevelData_SO)item.Clone());

            clonedObject.LevelDataList = clonedLevelDataList;

            return clonedObject;
        }
    }
}
