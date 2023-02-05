using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAchievement : MonoBehaviour
{
    public enum Achievement
    {
        FirstEnding,
        SecondEnding
    }
    
    private Achievement ach;

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;

        switch (ach)
        {
            case Achievement.FirstEnding : 
                PlayerPrefs.SetString("FirstEnding", "True");
                break;
            case Achievement.SecondEnding : 
                PlayerPrefs.SetString("SecondEnding", "True");
                break;
            default:
                Debug.Log("out of bound");
                break;
        }
        
    }
}
