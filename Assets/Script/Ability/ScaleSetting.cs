using System;
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

        private UIGamePlayController gameplayCtr;

        private void Awake()
        {
            SharedObject.Instance.Add(this);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            gameplayCtr = FindObjectOfType<UIGamePlayController>();

            UpdateScaleLabelUi();
        }

        public void UpdateScaleLabelUi()
        {
            switch (currentScale)
            {
                case 0 : 
                    gameplayCtr.SetSizeText("S","Small");
                    break;
                case 1 : 
                    gameplayCtr.SetSizeText("M","Medium");
                    break;
                case 2 : 
                    gameplayCtr.SetSizeText("L","Large");
                    break;
                default:
                    Debug.Log($"Size for txt out of bound!");
                    break;
            }
        }
    }
}