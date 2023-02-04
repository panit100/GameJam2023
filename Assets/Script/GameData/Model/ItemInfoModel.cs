using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.GameData
{
    public enum ItemType
    {
        None,
        Consumable,
        Equipable,
        Craftable,
    }

    [Serializable]
    public class ItemInfoModel 
    {
        [JsonProperty("id")]
        public string ID;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("item_type")]
        public ItemType Type;

        [JsonProperty("asset")]
        public string Asset;

        [JsonProperty("icon")]
        public string IconName;

        [JsonProperty("atlas_icon")]
        public string AtlasIcon;
    }
}

    
