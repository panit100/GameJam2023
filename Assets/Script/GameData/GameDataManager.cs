using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GameJam.Utilities;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameJam.GameData
{
    public class GameDataManager : MonoBehaviour
    {
        [SerializeField]
        private const string gameDataPath = "GameData/gameJam-gameData";

        private bool isInitialized;
        public bool IsIniialized => isInitialized;

        #region General Data

        [GameData("item_config")]
        private Dictionary<string, ItemInfoModel> itemConfig;

        [GameData("achievement_config")]
        private Dictionary<string, AchievementModel> achievementConfig;

        #endregion

        private void Awake()
        {
            SharedObject.Instance.Add(this);
        }

        public void StartInitialize()
        {
            LoadLocalGameConfig();
        }

        private void LoadLocalGameConfig()
        {
            var jsonFile = Resources.Load(gameDataPath) as TextAsset;

            ParseAndApplyGameDataFromJson(jsonFile.text);
        }

        private void ParseAndApplyGameDataFromJson(string jsonText)
        {
            var gameDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonText);

            var gameDataFields = typeof(GameDataManager).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.GetCustomAttributes(typeof(GameDataAttribute), true).Any()).ToList();

            foreach (var item in gameDataFields)
            {
                try
                {
                    var dictName = item.GetCustomAttribute(typeof(GameDataAttribute)) as GameDataAttribute;
                    var fieldGameDataString = gameDataDictionary[dictName.JsonElement];
                    var fieldType = item.FieldType;
                    var stringValue = fieldGameDataString.ToString();
                    var itemValue = JsonConvert.DeserializeObject(stringValue, fieldType);
                    item.SetValue(this, itemValue);

                }
                catch (Exception ex)
                {
                    Debug.Log($"Error parsing data: {item.Name}");
                    Debug.LogException(ex);
                }
            }

            // Debug.Log("================  Load GameData From Json Complete  =================");

            isInitialized = true;

            // Debug.Log(itemConfig["clock"].IconName);
        }

        public bool TryGetAchievementInfo(string achievementId, out AchievementModel achievementInfo)
        {
            bool exist = achievementConfig.TryGetValue(achievementId, out AchievementModel achievementModel);

            if (!exist)
            {
                Debug.LogError($"achievement ID : {achievementId} doesn't exist in current context!!");
                achievementInfo = new AchievementModel();
                return false;
            }

            achievementInfo = achievementModel;
            return true;
        }
    }
  

}


