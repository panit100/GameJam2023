using System.Collections;
using System.Collections.Generic;
using GameJam.Utilities;
using UnityEngine;

namespace GameJam.GameData
{
    public class ScaleSetting : MonoBehaviour
    {
        public float[] scaleValue;
        public int currentScale = 1;

        public enum SizeLabel
        {
            S,M,L,Small,Medium,Large
        }
        
        private void Awake()
        {
            SharedObject.Instance.Add(this);
            DontDestroyOnLoad(this);
        }
        
        
    }
}