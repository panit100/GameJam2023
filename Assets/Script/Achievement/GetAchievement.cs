using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAchievement : MonoBehaviour
{
    public enum Achievement
    {
        FirstEnding,
        SecondEnding,
        ClearAll
    }
    
    public Achievement ach;

    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;

        switch (ach)
        {
            case Achievement.FirstEnding : 
                Debug.Log($"First Ending");
                PlayerPrefs.SetString("FirstEnding", "True");
                break;
            case Achievement.SecondEnding : 
                Debug.Log($"Second Ending");
                PlayerPrefs.SetString("SecondEnding", "True");
                break;
            case Achievement.ClearAll : 
                Debug.Log($"Clear all Achievement");
                PlayerPrefs.SetString("FirstEnding", "False");
                PlayerPrefs.SetString("SecondEnding", "False");
                break;
            default:
                Debug.Log("out of bound");
                break;
        }
        
    }
}
