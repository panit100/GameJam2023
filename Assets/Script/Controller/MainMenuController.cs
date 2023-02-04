using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Utilities;

public class MainMenuController : MonoBehaviour
{
    SceneController sceneController;

    [SerializeField]
    private UIMainmenuPanel mainmenuPanel;

    [SerializeField]
    private UIAchievementPanel achievementPanel;

    [SerializeField]
    private UICreditPanel creditPanel;

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

        mainmenuPanel.Initialize(this);
        achievementPanel.Initialize(this);
        creditPanel.Initialize(this);

        isInitlize = true;
    }

    public void OnLoadSceneGameplay()
    {
        sceneController.OnLoadSceneGameplay();
    }

    public void OnClickAchievementButton()
    {
        Debug.Log("==========  Populate Achievement ==========");

        achievementPanel.Show();
    }

    public void OnClickCreditButton()
    {
        Debug.Log("==========  Populate  Credit  ==========");

        creditPanel.Show();
    }

    public void OnClickExitButton()
    {
        Debug.Log("==========  Exit Game  ==========");
    }
}

