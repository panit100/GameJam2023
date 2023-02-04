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

    [SerializeField] private GameObject playerRef; 

    public void CheckPlayerSize()
    {
        switch (buttonForPlayerSize)
        {
            case Size.big:
                if(playerRef.transform.localScale == Vector3.one * 2f) 
                    Debug.Log($"big");
                
                break;

            case Size.normal:
                if (playerRef.transform.localScale == Vector3.one)
                    Debug.Log($"med");

                break;

            case Size.small:
                if(playerRef.transform.localScale == Vector3.one * 0.5f) 
                    Debug.Log($"smol");
                
                break;
        }
    }

}
