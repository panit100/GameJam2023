using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.GameData
{
    [Serializable]
    public class AchievementModel
    {
        [JsonProperty("id")]
        public string ID;

        [JsonProperty("header")]
        public string Header;

        [JsonProperty("info")]
        public string Info;
    }
}
