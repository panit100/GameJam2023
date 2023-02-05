using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;

public enum SCENE
{
    SCENE2,
    SCENE3,
    Ending
}

public class Door : MonoBehaviour
{
    public SCENE loadScene;
    GameplayController gameplayController;

    void Start()
    {
        gameplayController = SharedObject.Instance.Get<GameplayController>();        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<PlayerMovementController>() != null)
        {
            switch(loadScene)
            {
                case SCENE.SCENE2:
                    gameplayController.OnLoadSceneMap2();
                    break;
                case SCENE.SCENE3:
                    gameplayController.OnLoadSceneMap3();
                    break;
                case SCENE.Ending:
                    gameplayController.OnLoadSceneGoodEnd();
                    break;
            }
            
        }
    }
}
