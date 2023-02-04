using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReciever : MonoBehaviour
{
    public enum Size
    {
        big, normal, small
    }
    
    public Size buttonForPlayerSize;

    private GameObject playerRef;
    private UIPrompt prompt;

    private void Start()
    {
        prompt = FindObjectOfType<UIPrompt>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    public void CheckPlayerSize()
    {
        switch (buttonForPlayerSize)
        {
            case Size.big:
                if(playerRef.transform.localScale == Vector3.one * 2f)
                    prompt.TextPrompt("Big");
                
                break;

            case Size.normal:
                if (playerRef.transform.localScale == Vector3.one)
                    prompt.TextPrompt("Medium");

                break;

            case Size.small:
                if(playerRef.transform.localScale == Vector3.one * 0.3f)
                    prompt.TextPrompt("Small");

                break;
        }
    }

}
