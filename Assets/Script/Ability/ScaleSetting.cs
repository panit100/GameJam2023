using System.Collections;
using System.Collections.Generic;
using GameJam.Utilities;
using UnityEngine;

namespace GameJam.GameData
{
    public class ScaleSetting : MonoBehaviour
    {
        [Tooltip("Set multiply value of scale UP")]
        public float scaleUpMultiply;
    
        [Tooltip("Set multiply value of scale DOWN")]
        public float scaleDownMultiply;
        
        [Tooltip("Set Normal Scale")]
        public float normalScale;

        public float[] scaleValue;
        public int currentScale = 1;
        
        private void Awake()
        {
            SharedObject.Instance.Add(this);
            DontDestroyOnLoad(this);
        }
    }
}