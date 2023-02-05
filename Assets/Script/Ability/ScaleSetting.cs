using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.Utilities;
using UnityEngine;

namespace GameJam.GameData
{
    public class ScaleSetting : MonoBehaviour
    {
        public float[] scaleValue = new float[3];
        public float[] jumpForceValue = new float[3];
        public int currentScale = 1;

        private UIGamePlayController gameplayCtr;
        private PlayerMovementController playerMovementCtr;

        private void Awake()
        {
            SharedObject.Instance.Add(this);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            gameplayCtr = FindObjectOfType<UIGamePlayController>();
            playerMovementCtr = FindObjectOfType<PlayerMovementController>();

            UpdateScaleLabelUi();
        }

        public void UpdateScaleLabelUi()
        {
            switch (currentScale)
            {
                case 0 : 
                    gameplayCtr.SetSizeText("S","Small");
                    playerMovementCtr.jumpForce = jumpForceValue[currentScale];
                    break;
                case 1 : 
                    gameplayCtr.SetSizeText("M","Medium");
                    playerMovementCtr.jumpForce = jumpForceValue[currentScale];
                    break;
                case 2 : 
                    gameplayCtr.SetSizeText("L","Large");
                    playerMovementCtr.jumpForce = jumpForceValue[currentScale];
                    break;
                default:
                    Debug.Log($"Size for txt out of bound!");
                    break;
            }
        }
    }
}