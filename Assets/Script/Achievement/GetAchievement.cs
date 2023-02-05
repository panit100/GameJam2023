using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;

public class GetAchievement : MonoBehaviour
{
    public enum Achievement
    {
        FirstEnding,
        SecondEnding,
        ClearAll
    }
    
    public Achievement ach;

    public SCENE loadScene;
    GameplayController gameplayController;

    void Start()
    {
        gameplayController = SharedObject.Instance.Get<GameplayController>();        
    }

    public void GetEnding()
    {
        switch (ach)
        {
            case Achievement.FirstEnding : 
                Debug.Log($"First Ending");
                PlayerPrefs.SetString("FirstEnding", "True");
                gameplayController.OnLoadSceneGoodEnd();
                break;
            case Achievement.SecondEnding : 
                Debug.Log($"Second Ending");
                PlayerPrefs.SetString("SecondEnding", "True");
                gameplayController.OnLoadSceneBadEnd();
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
