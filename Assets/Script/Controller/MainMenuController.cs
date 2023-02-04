using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Utilities;

public class MainMenuController : MonoBehaviour
{
    SceneController sceneController;

    bool isInitlize;

    void Awake() 
    {
        Initilize();
    }

    void Start()
    {
        sceneController = SharedObject.Instance.Get<SceneController>();
    }

    void Initilize()
    {
        SharedObject.Instance.Add(this);

        isInitlize = true;
    }

    public void OnLoadSceneGameplay()
    {
        sceneController.OnLoadSceneGameplay();
    }
}

